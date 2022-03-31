using Mortoff.Application.Common.Exceptions;
using Mortoff.Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mortoff.WebUI.Middlewares;

public class HttpStatusCodeExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILogger<HttpStatusCodeExceptionMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                throw;
            }

            var response = GetResponse(ex);

            _logger.LogDebug("HTTP Status Error ({StatusCode}): {@Payload}", response.statuscode, response.data);

            context.Response.Clear();
            context.Response.StatusCode = response.statuscode;
            context.Response.ContentType = @"application/json";

            var payload = JsonConvert.SerializeObject(response.data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await context.Response.WriteAsync(payload);
        }
    }

    private (ErrorResponse data, int statuscode) GetResponse(Exception ex)
    {
        var result = new ErrorResponse();

        int statuscode = StatusCodes.Status500InternalServerError;

        if (ex is HttpStatusCodeException hscEx)
        {
            result.Error = hscEx.Message;
            result.Details = ex is CommonHttpStatusException common ? common.Payload : null;
            statuscode = hscEx.StatusCode;
        }
        else if (ex is ValidationException validationException)
        {
            result.Error = "ERROR.VALIDATION";
            result.Details = validationException.Failures;
            statuscode = StatusCodes.Status400BadRequest;
        }
        else if (ex is NotImplementedException)
        {
            result.Error = ex.Message;
            statuscode = StatusCodes.Status501NotImplemented;
            _logger.LogCritical(ex, "Someone try to call a not implemented method");
        }
        else
        {
            result.Error = "ISE";
            _logger.LogError(ex, "Internal Server Error");
        }

        return (result, statuscode);
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
        public object Details { get; set; }
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
}
