namespace GroceryStore.Database.Entities.CategoryAttribute;

using GroceryStore.Database.Entities.Attribute;
using GroceryStore.Database.Entities.Category;

public class CategoryAttribute
{
    private CategoryAttribute()
    {
    }

    public int CategoryId { get; set; }

    public int AttributeId { get; set; }

    public bool IsRequired { get; set; }

    public Category Category { get; set; } = null!;

    public ProductAttribute Attribute { get; set; } = null!;
}