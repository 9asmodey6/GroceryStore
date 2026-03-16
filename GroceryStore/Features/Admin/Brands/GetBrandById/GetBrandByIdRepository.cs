namespace GroceryStore.Features.Admin.Brands.GetBrandById;

using Database;
using Microsoft.EntityFrameworkCore;

public class GetBrandByIdRepository(AppDbContext dbContext)
{
    public async Task<GetBrandByIdResponse?> GetAsync(int id, CancellationToken ct)
    {
        return await dbContext.Brands
            .AsNoTracking()
            .Where(b => b.Id == id)
            .Select(b => new GetBrandByIdResponse(b.Id, b.Name))
            .FirstOrDefaultAsync(ct);
    }
}