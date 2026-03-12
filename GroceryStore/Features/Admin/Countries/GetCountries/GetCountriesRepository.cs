namespace GroceryStore.Features.Admin.Countries.GetCountries;

using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;

public class GetCountriesRepository(AppDbContext dbContext, IMemoryCache cache)
{
    private const string CacheKey = LookupCacheKeys.Countries;

    public async ValueTask<List<GetCountriesResponse>> GetCountriesAsync(CancellationToken ct)
    {
        var countries = await cache.GetOrCreateAsync(
            CacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await dbContext.Countries
                    .AsNoTracking()
                    .OrderBy(c => c.Id)
                    .Select(c => new GetCountriesResponse(
                        c.Id,
                        c.Name,
                        c.Code))
                    .ToListAsync(ct);
            }) ?? [];

        return countries;
    }
}