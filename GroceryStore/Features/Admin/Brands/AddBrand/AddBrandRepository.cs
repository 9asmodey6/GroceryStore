namespace GroceryStore.Features.Admin.Brands.AddBrand;

using Database;
using Database.Entities.Brand;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;

public class AddBrandRepository(AppDbContext dbContext) : IRepository
{
    public async Task AddBrandAsync(Brand brand, CancellationToken ct)
    {
        dbContext.Brands.Add(brand);
        await dbContext.SaveChangesAsync(ct);
    }
}