using Microsoft.EntityFrameworkCore;
using Mortoff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Application.Interfaces;

public interface IAppdDbContext
{
    DbSet<StockEntity> Stocks { get; set; }
    DbSet<DataEntity> Datas { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
