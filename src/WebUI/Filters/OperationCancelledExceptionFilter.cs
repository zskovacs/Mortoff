﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mortoff.WebUI.Filters;

public class OperationCancelledExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public OperationCancelledExceptionFilter(ILogger<OperationCancelledExceptionFilter> logger)
    {
        _logger = logger;
    }
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is OperationCanceledException)
        {
            _logger.LogInformation("Request was cancelled");
            context.ExceptionHandled = true;
            context.Result = new StatusCodeResult(400);
        }
    }
}
