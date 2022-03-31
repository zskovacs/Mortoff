using Microsoft.AspNetCore.Mvc;
using Mortoff.Application.Stock.Commands;
using static Mortoff.WebUI.Middlewares.HttpStatusCodeExceptionMiddleware;

namespace Mortoff.WebUI.Controllers;

[Route("api/[controller]")]
public class StockController : BaseController
{

    [HttpPost]
    [Route("upload-stock-data")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadStockData([FromForm] UploadStockDataCommand request)
    {
        var response = await Send(request);
        return Ok(response);
    }
}
