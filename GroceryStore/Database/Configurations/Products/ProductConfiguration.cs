namespace GroceryStore.Database.Configurations.Products;

using System.Text.Json;
using Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasQueryFilter(p => p.IsActive); // Filter by active Products

        builder.Property(p => p.Metadata)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<int, string>>(v, (JsonSerializerOptions?)null)
                     ?? new())
            .Metadata.SetValueComparer(
                new ValueComparer<Dictionary<int, string>>(
                    (d1, d2) => d1!.SequenceEqual(d2!),
                    d => d.Aggregate(0, (a, v) =>
                        HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
                    d => d.ToDictionary(entry => entry.Key, entry => entry.Value)
                ));

        builder.Property(p => p.Metadata)
            .HasDefaultValueSql("'{}'::jsonb");

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);

        builder.HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId);

        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Price).HasPrecision(18, 2);

        builder.HasIndex(p => p.SKU).IsUnique();

        builder.Property(p => p.BaseUnit).HasDefaultValue("pcs");
    }
}