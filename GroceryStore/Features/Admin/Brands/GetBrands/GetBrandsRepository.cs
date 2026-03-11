namespace GroceryStore.Features.Admin.Brands.GetBrands;

using Database;
using Database.Entities.Brand;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

public class GetBrandsRepository(AppDbContext dbContext)
{
    public async Task<List<GetBrandsResponse>> GetBrandsAsync(CancellationToken ct)
    {
        var brands = await dbContext.Brands
            .AsNoTracking()
            .Select(b => new GetBrandsResponse(b.Id, b.Name))
            .ToListAsync(ct);

        return brands;
    }
}