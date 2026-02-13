using GroceryStore.Shared;

namespace GroceryStore.Domain;

public class Category : BaseEntity
{
    required public string Name { get; set; }

    public int? ParentId { get; private set; }

    public ICollection<CategoryAttribute> CategoryAttributes { get; set; } = new List<CategoryAttribute>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}