using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mortoff.Domain.Entities;

namespace Mortoff.Persistence.Configurations;
internal class DataEntityConfiguration : IEntityTypeConfiguration<DataEntity>
{
    public void Configure(EntityTypeBuilder<DataEntity> builder)
    {
        builder.ToTable("Datas");

        builder.Property(x => x.Date).HasColumnType("DATE");

        builder.Property(x => x.Open).HasColumnType("DECIMAL(11,6)");
        builder.Property(x => x.Close).HasColumnType("DECIMAL(11,6)");
        builder.Property(x => x.High).HasColumnType("DECIMAL(11,6)");
        builder.Property(x => x.Low).HasColumnType("DECIMAL(11,6)");

        builder.HasOne(x => x.Stock).WithMany(x => x.Datas).HasForeignKey(x => x.StockId).OnDelete(DeleteBehavior.Cascade);
    }
}
