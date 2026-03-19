namespace GroceryStore.Features.Admin.Brands.DeleteBrand;

using Database;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

public class DeleteBrandRepository(AppDbContext dbContext) : IRepository
{
    public async Task<bool> DeleteBrandAsync(int brandId, CancellationToken ct)
    {
        var affectedRows = await dbContext.Brands
            .Where(b => b.Id == brandId)
            .ExecuteDeleteAsync(ct);

        return affectedRows > 0;
    }
}