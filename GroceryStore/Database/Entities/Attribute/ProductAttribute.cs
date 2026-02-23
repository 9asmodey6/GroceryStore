namespace GroceryStore.Database.Entities.Attribute;

using Enums;
using Shared;

public class ProductAttribute : BaseEntity
{
    private ProductAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public AttributeDataType DataType { get; set; }

    public string? Unit { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }
}