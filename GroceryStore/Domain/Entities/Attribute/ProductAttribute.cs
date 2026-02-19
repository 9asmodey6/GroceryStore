namespace GroceryStore.Domain.Entities.Attribute;

using Category;
using GroceryStore.Domain.Enums;
using Shared;

public class ProductAttribute : BaseEntity
{
    public string Name { get; set; }

    public AttributeDataType DataType { get; set; }

    public string? Unit { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    private ProductAttribute()
    {
    }
}