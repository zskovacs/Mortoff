using Microsoft.EntityFrameworkCore;
using Mortoff.Domain.Entities;

namespace Mortoff.Application.Interfaces;

public interface IAppdDbContext
{
    DbSet<StockEntity> Stocks { get; set; }
    DbSet<DataEntity> Datas { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
