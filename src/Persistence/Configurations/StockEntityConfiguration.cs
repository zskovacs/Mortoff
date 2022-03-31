using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mortoff.Domain.Entities;

namespace Mortoff.Persistence.Configurations;
internal class StockEntityConfiguration : IEntityTypeConfiguration<StockEntity>
{
    public void Configure(EntityTypeBuilder<StockEntity> builder)
    {
        builder.ToTable("Stocks");
        builder.Property(x => x.Name).HasMaxLength(40);
    }
}
