using Dapper;
using Mortoff.Application.Stock.Models;

namespace Mortoff.Application.Stock.Queries;
public record StockListQuery() : IRequest<List<StockListViewModel>>;

public class StockListQueryHandler : IRequestHandler<StockListQuery, List<StockListViewModel>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public StockListQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<StockListViewModel>> Handle(StockListQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.GetOpenConnection();

        var sql = @"SELECT 
	                s.[Name]
	                ,MIN(d.[Date]) as [From]
	                ,MAX(d.[Date]) as [To]
                  FROM [dbo].[Stocks] s
                  LEFT OUTER JOIN [dbo].[Datas] d ON s.Id = d.StockId
                  GROUP BY s.[Name]";

        var result = await connection.QueryAsync<StockListViewModel>(sql);

        return result.ToList();
    }
}