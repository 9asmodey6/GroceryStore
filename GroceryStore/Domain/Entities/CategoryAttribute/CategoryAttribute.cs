namespace GroceryStore.Domain.Entities.CategoryAttribute;

using Attribute;
using Category;

public class CategoryAttribute
{
    public int CategoryId { get; set; }

    public int AttributeId { get; set; }

    public bool IsRequired { get; set; }

    public Category Category { get; set; } = null!;

    public ProductAttribute Attribute { get; set; } = null!;

    private CategoryAttribute()
    {
    }
}