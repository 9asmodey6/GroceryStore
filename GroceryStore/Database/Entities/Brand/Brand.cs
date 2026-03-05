namespace GroceryStore.Database.Entities.Brand;

using Product;
using Shared;

public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}