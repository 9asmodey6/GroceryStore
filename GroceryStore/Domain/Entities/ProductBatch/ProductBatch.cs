namespace GroceryStore.Domain.Entities.ProductBatch;

using GroceryStore.Domain.Entities.Shared;
using Product;

public class ProductBatch : BaseEntity
{
    public int ProductId { get; private set; }

    public int QuantityArrived { get; set; }

    public int QuantityRemaining { get; set; }

    public string BatchNumber { get; private set; }

    public Product Product { get; private set; }

    public decimal PurchasePrice { get; private set; }

    public DateOnly SupplyDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    public DateTime ExpirationDate { get; private set; }

    public bool IsClosed { get; set; } = false;

    private ProductBatch()
    {
    }
}