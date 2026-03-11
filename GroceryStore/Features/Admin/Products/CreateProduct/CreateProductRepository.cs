namespace GroceryStore.Features.Admin.Products.CreateProduct;

using GroceryStore.Database;
using GroceryStore.Database.Entities.Product;

public class CreateProductRepository(AppDbContext dbContext)
{
    public async Task CreateAsync(Product product, CancellationToken ct)
    {
        dbContext.Add(product);
        await dbContext.SaveChangesAsync(ct);
    }
}