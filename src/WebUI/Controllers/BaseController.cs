using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mortoff.WebUI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult Created(object data) => new ObjectResult(data)
    {
        StatusCode = StatusCodes.Status201Created
    };

    protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request) => Mediator.Send(request, HttpContext.RequestAborted);
}
