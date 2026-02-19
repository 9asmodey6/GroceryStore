namespace GroceryStore.Infrastructure.Configurations.Products;

using System.Text.Json;
using GroceryStore.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Metadata)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.
                    Deserialize<Dictionary<int, string>>(v, (JsonSerializerOptions?)null)
                     ?? new());

        builder.Property(p => p.Metadata)
            .HasDefaultValueSql("'{}'::jsonb");

        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.SKU).IsUnique();

        builder.Property(p => p.BaseUnit).HasDefaultValue("pcs");
    }
}