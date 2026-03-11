namespace GroceryStore.Database.Entities.Brand;

using Product;
using Shared;

public class Brand : BaseEntity
{
    public Brand(string name)
    {
        Name = name;
    }

    public string Name { get; set; } 
    public ICollection<Product> Products { get; set; } = new List<Product>();
}