namespace GroceryStore.Database.Configurations.Products;

using GroceryStore.Database.Entities.Attribute;
using GroceryStore.Database.Enums;
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
                Id = 12,
                Name = "Energy Value",
                DataType = AttributeDataType.Decimal,
                Unit = "kcal/100g",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1000,
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
                Id = 14,
                Name = "Is Vegan",
                DataType = AttributeDataType.Boolean,
                Unit = (string?)null,
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1,
            },
            new
            {
                Id = 15,
                Name = "Pieces per Pack",
                DataType = AttributeDataType.Integer,
                Unit = "pcs",
                MinValue = (decimal?)1,
                MaxValue = (decimal?)100,
            },
            new
            {
                Id = 16,
                Name = "Temperature Storage",
                DataType = AttributeDataType.Decimal,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 17,
                Name = "Cut Type",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 18,
                Name = "Meat Part",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 19,
                Name = "Fish Type",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 20,
                Name = "Processing Method",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 21,
                Name = "Grain Type",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 22,
                Name = "Shelf Life",
                DataType = AttributeDataType.Integer,
                Unit = "days",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)365,
            },
            new
            {
                Id = 23,
                Name = "Protein Content",
                DataType = AttributeDataType.Decimal,
                Unit = "%",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)100,
            },
            new
            {
                Id = 24,
                Name = "Sweetness Level",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 25,
                Name = "Variety",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 26,
                Name = "Gluten Free",
                DataType = AttributeDataType.Boolean,
                Unit = (string?)null,
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1,
            },
            new
            {
                Id = 27,
                Name = "Packaging Type",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            },
            new
            {
                Id = 28,
                Name = "Caffeine Content",
                DataType = AttributeDataType.Decimal,
                Unit = "mg",
                MinValue = (decimal?)0,
                MaxValue = (decimal?)500,
            },
            new
            {
                Id = 29,
                Name = "Carbonation",
                DataType = AttributeDataType.Boolean,
                Unit = (string?)null,
                MinValue = (decimal?)0,
                MaxValue = (decimal?)1,
            },
            new
            {
                Id = 30,
                Name = "Chemical Type",
                DataType = AttributeDataType.String,
                Unit = (string?)null,
                MinValue = (decimal?)null,
                MaxValue = (decimal?)null,
            }
        );
    }
}