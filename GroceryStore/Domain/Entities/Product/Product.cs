using System.Text;
using GroceryStore.Shared;

namespace GroceryStore.Domain;

public class Product : BaseEntity
{
    public string Name { get; private set; } = null!;

    public decimal Price { get; private set; }

    public int CategoryId { get; private set; } // FK

    public Category Category { get; set; } = null!; // Navigation property

    public string SKU { get; private set; } = null!;

    public string? Description { get; private set; }

    public string BaseUnit { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public Dictionary<string, string> Details { get; private set; } = new();

    private Product()
    {
    }

    public Product(string name, decimal price)
    {
        SetName(name);
        UpdatePrice(price);
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price must be non-negative", nameof(newPrice));
        }

        Price = newPrice;
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }

    public string GetFullInfo()
    {
        var sb = new StringBuilder();
        sb.Append($"Category: {Category.Name}\n{Name}: {Price}UAH\n");
        foreach (var pair in Details)
        {
            sb.AppendLine($"{pair.Key}: {pair.Value}");
        }

        return sb.ToString();
    }
 }