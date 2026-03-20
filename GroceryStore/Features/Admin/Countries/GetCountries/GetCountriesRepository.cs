namespace GroceryStore.Features.Admin.Countries.GetCountries;

using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;
using Shared.Consts.CacheKeys;
using Shared.Interfaces;

public class GetCountriesRepository(AppDbContext dbContext, IMemoryCache cache) : IRepository
{
    public async ValueTask<GetCountriesResponse> GetCountriesAsync(CancellationToken ct)
    {
        var items = await cache.GetOrCreateAsync(
            LookupCacheKeys.Countries,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                return await dbContext.Countries
                    .AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Select(c => new GetCountriesResponseItem(
                        c.Id,
                        c.Name,
                        c.Code))
                    .ToArrayAsync(ct);
            });
        
        return new GetCountriesResponse(items ?? Array.Empty<GetCountriesResponseItem>());
    }
}