using GroceryStore.Shared;

namespace GroceryStore.Domain;

public class StockBatch : BaseEntity
{
    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public int RemainingQuantity { get; set; }

    public string BatchNumber { get; private set; }

    public decimal PurchasePrice { get; private set; }

    public DateOnly SupplyDate { get; private set; }

    public DateTime ExpirationDate { get; private set; }

    private StockBatch()
    {
    }
}