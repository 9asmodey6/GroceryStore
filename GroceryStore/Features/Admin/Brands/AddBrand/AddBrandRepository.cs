namespace GroceryStore.Features.Admin.Brands.AddBrand;

using Database;
using Database.Entities.Brand;

public class AddBrandRepository(AppDbContext dbContext)
{
    public async Task AddBrandAsync(Brand brand, CancellationToken ct)
    {
        dbContext.Brands.Add(brand);
        await dbContext.SaveChangesAsync(ct);
    }
}