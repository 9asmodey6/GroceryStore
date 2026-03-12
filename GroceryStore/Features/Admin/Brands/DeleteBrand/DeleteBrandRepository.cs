namespace GroceryStore.Features.Admin.Brands.DeleteBrand;

using Database;

public class DeleteBrandRepository(AppDbContext dbContext)
{
    public async Task<bool> DeleteBrandAsync(int brandId, CancellationToken ct)
    {
        var brand = await dbContext.Brands.FindAsync(brandId, ct);
        if (brand != null)
        {
            dbContext.Brands.Remove(brand);
            await dbContext.SaveChangesAsync(ct);
            return true;
        }

        return false;
    }
}