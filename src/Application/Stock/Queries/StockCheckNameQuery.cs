using Microsoft.EntityFrameworkCore;
using Mortoff.Application.Common.Validators;

namespace Mortoff.Application.Stock.Queries;
public record StockCheckNameQuery(string Name) : IRequest<bool>;

internal class CheckStockNameQueryHandler : IRequestHandler<StockCheckNameQuery, bool>
{
    private readonly IAppdDbContext _dbContext;

    public CheckStockNameQueryHandler(IAppdDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> Handle(StockCheckNameQuery request, CancellationToken cancellationToken) => _dbContext.Stocks.AnyAsync(x => x.Name == request.Name, cancellationToken);
}

internal class CheckStockNameQueryValidator : AbstractValidator<StockCheckNameQuery>
{
    public CheckStockNameQueryValidator()
    {
        RuleFor(x => x.Name).SetValidator(new StockNameValidator());
    }
}