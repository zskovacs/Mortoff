using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mortoff.WebUI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private ISender? _mediator;

    private ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;

    protected ActionResult Created(object data) => new ObjectResult(data)
    {
        StatusCode = StatusCodes.Status201Created
    };

    protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request) =>
        Mediator.Send(request, HttpContext.RequestAborted);
    
    protected Task Send(IRequest request) =>
        Mediator.Send(request, HttpContext.RequestAborted);
}
