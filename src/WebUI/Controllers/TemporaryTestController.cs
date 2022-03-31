using Microsoft.AspNetCore.Mvc;
using static Mortoff.WebUI.Middlewares.HttpStatusCodeExceptionMiddleware;

namespace Mortoff.WebUI.Controllers;

[Route("api/[controller]")]
public class TemporaryTestController : BaseController
{
    [HttpGet]
    [Route("ok")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> TestOk()
    {        
        return Ok("response OK");
    }

    [HttpGet]
    [Route("nok")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> TestNok()
    {
        throw new BadHttpRequestException("Testing exception");
        return Ok();
    }
}
