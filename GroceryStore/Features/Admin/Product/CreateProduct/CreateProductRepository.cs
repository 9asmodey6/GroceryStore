namespace GroceryStore.Features.Admin.Product.CreateProduct;

using Database.Entities.Product;
using Database;

public class CreateProductRepository(AppDbContext dbContext)
{
    public async Task CreateAsync(Product product, CancellationToken ct)
    {
        await dbContext.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}