using Microsoft.AspNetCore.Mvc;
using Mortoff.Application.Stock.Commands;
using Mortoff.Application.Stock.Models;
using Mortoff.Application.Stock.Queries;
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

    [HttpGet]
    [Route("get-stock-history")]
    [ProducesResponseType(typeof(List<StockDataViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistory([FromQuery] StockDataQuery request)
    {
        var response = await Send(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("check-stock-exist/{name}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckStock(string name)
    {
        var response = await Send(new CheckStockNameQuery(name));
        return Ok(response);
    }

    [HttpGet]
    [Route("list-available-stocks")]
    [ProducesResponseType(typeof(List<StockListViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListStocks()
    {
        var response = await Send(new StockListQuery());
        return Ok(response);
    }
}
