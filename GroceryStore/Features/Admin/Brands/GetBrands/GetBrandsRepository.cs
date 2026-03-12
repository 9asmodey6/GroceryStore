namespace GroceryStore.Features.Admin.Brands.GetBrands;

using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;

public class GetBrandsRepository(AppDbContext dbContext, IMemoryCache cache)
{
    private const string CacheKey = LookupCacheKeys.Brands;

    public async ValueTask<List<GetBrandsResponse>> GetBrandsAsync(CancellationToken ct)
    {
        var brands = await cache.GetOrCreateAsync(
            CacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                return await dbContext.Brands
                    .AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Select(b => new GetBrandsResponse(b.Id, b.Name))
                    .ToListAsync(ct);
            }) ?? [];

        return brands;
    }
}