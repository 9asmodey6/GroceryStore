namespace GroceryStore.Features.Admin.Brands.GetBrands;

using Database;
using Database.Entities.Brand;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;
using Shared.Consts.CacheKeys;
using Shared.Interfaces;

public class GetBrandsRepository(AppDbContext dbContext, IMemoryCache cache) : IRepository
{
    public async ValueTask<GetBrandsResponse> GetBrandsAsync(CancellationToken ct)
    {
        var items = await cache.GetOrCreateAsync(
            LookupCacheKeys.Brands,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

                return await dbContext.Brands
                    .AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Select(b => new GetBrandsResponseItem(b.Id, b.Name))
                    .ToArrayAsync(ct);
            });

        return new GetBrandsResponse(items ?? []);
    }
}