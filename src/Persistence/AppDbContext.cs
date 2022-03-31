using Microsoft.EntityFrameworkCore;
using Mortoff.Application.Interfaces;
using Mortoff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mortoff.Persistence;
public class AppDbContext : DbContext, IAppdDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<StockEntity> Stocks { get; set; }
    public DbSet<DataEntity> Datas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
