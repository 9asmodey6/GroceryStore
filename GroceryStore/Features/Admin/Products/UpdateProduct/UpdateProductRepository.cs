namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using GroceryStore.Database;
using GroceryStore.Database.Entities.Product;
using Microsoft.EntityFrameworkCore;

public class UpdateProductRepository(AppDbContext context)
{
    public async Task<Product?> GetById(int productId, CancellationToken ct)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == productId, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await context.SaveChangesAsync(ct);
    }
}