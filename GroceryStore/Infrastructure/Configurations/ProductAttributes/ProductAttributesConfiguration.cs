namespace GroceryStore.Infrastructure.Configurations.ProductAttributes;

using Domain.Entities.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductsAttributesConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.DataType)
            .HasConversion<int>()
            .IsRequired();
    }
}