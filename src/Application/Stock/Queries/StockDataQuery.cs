using Mortoff.Application.Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Stock.Queries;
public record StockDataQuery(string Name) : IRequest<StockDataViewModel>;

internal class StockDataQueryHandler : IRequestHandler<StockDataQuery, StockDataViewModel>
{
    private readonly IAppdDbContext _dbContext;

    public StockDataQueryHandler(IAppdDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<StockDataViewModel> Handle(StockDataQuery request, CancellationToken cancellationToken)
    {

    }
}

