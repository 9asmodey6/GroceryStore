namespace GroceryStore.Database.Entities.Product;

using Category;
using Shared;

public class Product : BaseEntity
{
    public Product(
        string name,
        decimal price,
        int categoryId,
        string sku,
        string? description,
        string? baseUnit,
        Dictionary<int, string> metadata)
    {
        SetName(name);
        UpdatePrice(price);
        SetCategoryId(categoryId);
        UpdateSku(sku);
        UpdateDescription(description);
        SetBaseUnit(baseUnit);
        SetMetadata(metadata);
    }

    private Product()
    {
    }

    public string Name { get; private set; } = null!;

    public decimal Price { get; private set; }

    public int CategoryId { get; private set; } // FK

    public Category Category { get; private set; } = null!; // Navigation property

    public string SKU { get; private set; } = null!;

    public string? Description { get; private set; }

    public string? BaseUnit { get; private set; } = "pcs";

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public bool IsActive { get; private set; } = true;

    public Dictionary<int, string> Metadata { get; private set; } = new ();

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price must be non-negative", nameof(newPrice));
        }

        Price = newPrice;
        Touch();
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
        Touch();
    }

    public void SetCategoryId(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("CategoryId must be non-negative", nameof(categoryId));
        }

        CategoryId = categoryId;
        Touch();
    }

    public void UpdateSku(string sku)
    {
        ArgumentNullException.ThrowIfNull(sku);
        SKU = sku;
        Touch();
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
        Touch();
    }

    public void SetBaseUnit(string? baseUnit)
    {
        BaseUnit = string.IsNullOrWhiteSpace(baseUnit) ? "pcs" : baseUnit;
        Touch();
    }

    public void SetMetadata(Dictionary<int, string> metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata);
        Metadata = metadata;
        Touch();
    }

    public void SoftDelete()
    {
        IsActive = false;
        Touch();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}