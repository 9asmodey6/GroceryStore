namespace GroceryStore.Features.Admin.Products.DeleteProduct;

using GroceryStore.Database;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;

public class DeleteProductRepository(AppDbContext dbContext) : IRepository
{
    public async Task<bool> DeleteProductByIdAsync(int productId,  CancellationToken ct)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId, ct);
        if (product == null || !product.IsActive)
        {
            return false;
        }

        product.SoftDelete();
        await dbContext.SaveChangesAsync(ct);
        return true;
    }
}