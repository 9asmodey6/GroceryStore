namespace GroceryStore.Database.Entities.ProductBatch;

using Product;
using Shared;

public class ProductBatch : BaseEntity
{
    private ProductBatch(string batchNumber, Product product)
    {
        BatchNumber = batchNumber;
        Product = product;
    }

    public int QuantityArrived { get; set; }

    public int QuantityRemaining { get; set; }

    public string BatchNumber { get; private set; }

    public int ProductId { get; private set; }

    public Product Product { get; private set; }

    public decimal PurchasePrice { get; private set; }

    public DateOnly SupplyDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

    public DateOnly ExpirationDate { get; private set; }

    public bool IsClosed { get; set; } = false;

    private ProductBatch()
    {
    }
}