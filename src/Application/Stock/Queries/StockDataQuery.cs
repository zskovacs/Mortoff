using Dapper;
using Mortoff.Application.Common.Validators;
using Mortoff.Application.Stock.Models;

namespace Mortoff.Application.Stock.Queries;
public record StockDataQuery(string Name, DateTime From, DateTime To) : IRequest<List<StockDataViewModel>>;

internal class StockDataQueryHandler : IRequestHandler<StockDataQuery, List<StockDataViewModel>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public StockDataQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<StockDataViewModel>> Handle(StockDataQuery request, CancellationToken cancellationToken)
    {
        // Adatok lekéréséhez DAPPERt használunk, így sokkal gyorsabb a query és a mappelés mint a sima EF-nél
        using var connection = _connectionFactory.GetOpenConnection();

        var sql = @"SELECT d.[Date]
                          ,d.[Open]
                          ,d.[High]
                          ,d.[Low]
                          ,d.[Close]
                          ,d.[Volume]      
                      FROM [dbo].[Datas] d
                      JOIN [dbo].[Stocks] s ON s.Id = d.StockId
                      WHERE s.[Name] = @StockName AND d.[Date] BETWEEN @From AND @To";

        var result = await connection.QueryAsync<StockDataViewModel>(sql, new { StockName = request.Name, From = request.From, To = request.To });

        return result.ToList();
    }
}

internal class StockDataQueryValidator : AbstractValidator<StockDataQuery>
{
    public StockDataQueryValidator()
    {
        RuleFor(x => x.Name).SetValidator(new StockNameValidator());
    }
}