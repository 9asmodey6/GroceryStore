namespace GroceryStore.Database.Entities.Category;

using CategoryAttribute;
using Product;
using Shared;

public class Category : BaseEntity
{
    required public string Name { get; set; }

    public int? ParentId { get; set; }

    public Category? Parent { get; set; }

    public ICollection<CategoryAttribute> Attributes { get; set; } = new List<CategoryAttribute>();

    public ICollection<Category> Children { get; set; } = new List<Category>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}