using System.Text;
using GroceryStore.Shared;

namespace GroceryStore.Domain;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Category Category { get; set; } = null!;
    public Dictionary<string, string> Details { get; private set; }
    private Product() { }

    public Product(string name, decimal price)
    {
        SetName(name);
        UpdatePrice(price);
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
        }

        Price = newPrice;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Enter a valid product name.", nameof(name));
        }

        Name = name;
    }

    public string GetFullInfo()
    {
        var sb = new StringBuilder();
        sb.Append($"Category: {Category.Name}\n{Name}: {Price}UAH\n");
        if (Details != null && Details.Count > 0)
        {
            foreach (var pair in Details)
            {
                sb.AppendLine($"{pair.Key}: {pair.Value}");
            }
        }

        return sb.ToString();
    }
}