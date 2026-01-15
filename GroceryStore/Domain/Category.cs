using GroceryStore.Shared;

namespace GroceryStore.Domain;

public class Category : BaseEntity
{
    required public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}