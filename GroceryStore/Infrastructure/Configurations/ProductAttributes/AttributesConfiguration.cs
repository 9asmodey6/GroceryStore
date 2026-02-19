namespace GroceryStore.Infrastructure.Configurations.ProductAttributes;

using Domain.Entities.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductsAttributesConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.ToTable("attributes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnName("name").IsRequired();

        builder.Property(x => x.DataType)
            .HasColumnName("data_type")
            .HasConversion<int>(); // в БД у тебя 123 на diagram, значит int

        builder.Property(x => x.Unit).HasColumnName("unit");
        builder.Property(x => x.MinValue).HasColumnName("min_value");
        builder.Property(x => x.MaxValue).HasColumnName("max_value");
    }
}