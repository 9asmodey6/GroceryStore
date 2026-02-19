namespace GroceryStore.Domain.Entities.Category;

using GroceryStore.Domain.Entities.Attribute;
using Product;
using Shared;

public class Category : BaseEntity
{
    required public string Name { get; set; }

    public int? ParentId { get; set; }

    public ICollection<CategoryAttribute> CategoryAttributes { get; set; } = new List<CategoryAttribute>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}