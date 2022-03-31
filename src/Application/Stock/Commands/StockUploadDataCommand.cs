using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mortoff.Application.Common.Validators;
using Mortoff.Domain.Entities;
using Mortoff.Domain.Import;

namespace Mortoff.Application.Stock.Commands;
public record StockUploadDataCommand(IFormFile File, string StockName) : IRequest;

internal class UploadStockDataCommandHandler : IRequestHandler<StockUploadDataCommand>
{
    private readonly IAppdDbContext _dbContext;
    private readonly IFileParser<StockCsvRecord> _csvParser;
    private readonly IMapper _mapper;

    public UploadStockDataCommandHandler(IAppdDbContext dbContext, IFileParser<StockCsvRecord> csvParser, IMapper mapper)
    {
        _dbContext = dbContext;
        _csvParser = csvParser;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(StockUploadDataCommand request, CancellationToken cancellationToken)
    {
        var records = _csvParser.ParseFile(request.File);

        var stock = await _dbContext.Stocks.Include(x => x.Datas).Where(x => x.Name == request.StockName).FirstOrDefaultAsync(cancellationToken);
        if (stock == null)
        {
            stock = new StockEntity() { Name = request.StockName };
            await _dbContext.Stocks.AddAsync(stock, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        stock.Datas = _mapper.Map<List<DataEntity>>(records);

        _dbContext.Stocks.Update(stock);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

internal class UploadStockDataCommandValidator : AbstractValidator<StockUploadDataCommand>
{
    public UploadStockDataCommandValidator()
    {
        RuleFor(x => x.StockName).SetValidator(new StockNameValidator());

        RuleFor(x => x.File)
            .NotNull().WithMessage("Nem található a feltöltött file")
            .SetValidator(new FileValidator(2048000, new List<string> { "text/csv", "application/vnd.ms-excel" }));
    }
}
