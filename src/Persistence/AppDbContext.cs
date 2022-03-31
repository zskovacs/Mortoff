using Microsoft.EntityFrameworkCore;
using Mortoff.Application.Interfaces;
using Mortoff.Domain.Entities;

namespace Mortoff.Persistence;
public class AppDbContext : DbContext, IAppdDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<StockEntity> Stocks { get; set; }
    public DbSet<DataEntity> Datas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
