using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Stock.Queries;
public record CheckStockNameQuery(string Name) : IRequest<bool>;

internal class CheckStockNameQueryHandler : IRequestHandler<CheckStockNameQuery, bool>
{
    private readonly IAppdDbContext _dbContext;

    public CheckStockNameQueryHandler(IAppdDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<bool> Handle(CheckStockNameQuery request, CancellationToken cancellationToken) => _dbContext.Stocks.AnyAsync(x => x.Name == request.Name, cancellationToken);
}

internal class CheckStockNameQueryValidator : AbstractValidator<CheckStockNameQuery>
{
    public CheckStockNameQueryValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(40).WithMessage(x => $"Részvény neve nem hosszabb-e mint 40 karakter. Amit megadtál {x.Name.Length} hosszú")
            .Matches(@"^[A-Za-z0-9\s]*$").WithMessage("A részvény neve csak az angol ABC betűit, szóközt és számokat tartalmazhat");
    }
}