namespace GroceryStore.Database.Configurations.ProductAttributes;

using Entities.Attribute;
using Enums;
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

        // Attributes (ProductAttribute)
        builder.HasData(
            new
            {
                Id = 1,
                Name = "Fat Content",
                DataType = AttributeDataType.Decimal,
                Unit = "%",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)100,
            },
            new
            {
                Id = 2,
                Name = "Weight",
                DataType = AttributeDataType.Integer,
                Unit = "g",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)10000,
            },
            new
            {
                Id = 3,
                Name = "Volume",
                DataType = AttributeDataType.Integer,
                Unit = "ml",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)10000,
            },
            new
            {
                Id = 4,
                Name = "Milk Source",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 5,
                Name = "Base Ingredient",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 6,
                Name = "Flavor",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 7,
                Name = "Mold Type",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 8,
                Name = "Lactose Free",
                DataType = AttributeDataType.Boolean,
                Unit = (string?)null,
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1,
            },
            new
            {
                Id = 9,
                Name = "Sugar Content",
                DataType = AttributeDataType.Decimal,
                Unit = "%",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)100,
            },
            new
            {
                Id = 10,
                Name = "Contains Filling",
                DataType = AttributeDataType.Boolean,
                Unit = (string?)null,
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1,
            },
            new
            {
                Id = 11,
                Name = "Alcohol Content",
                DataType = AttributeDataType.Decimal,
                Unit = "%",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)100,
            },
            new
            {
                Id = 13,
                Name = "Organic",
                DataType = AttributeDataType.Boolean,
                Unit = (string?)null,
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1,
            },
            new
            {
                Id = 16,
                Name = "Temperature Storage",
                DataType = AttributeDataType.Decimal,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            }
        );
    }
}