namespace GroceryStore.Infrastructure.Configurations.ProductBatches;

using Domain.Entities.Attribute;
using Domain.Entities.ProductBatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductBatchConfiguration : IEntityTypeConfiguration<ProductBatch>
{
    public void Configure(EntityTypeBuilder<ProductBatch> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BatchNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.QuantityArrived)
            .IsRequired();

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.ExpirationDate)
            .IsRequired();

        builder.Property(x => x.SupplyDate)
            .IsRequired();

        builder.Property(x => x.IsClosed).HasDefaultValue(false);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ProductId, x.ExpirationDate, x.SupplyDate });
    }
}