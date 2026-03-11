namespace GroceryStore.Features.Admin.Countries.GetCountries;

using Database;
using Microsoft.EntityFrameworkCore;

public class GetCountriesRepository(AppDbContext dbContext)
{
    public async Task<List<GetCountriesResponse>> GetCountriesAsync(CancellationToken ct)
    {
        var countries = await dbContext.Countries
            .AsNoTracking()
            .Select(c => new GetCountriesResponse(
                c.Id,
                c.Name,
                c.Code))
            .ToListAsync(ct);

        return countries;
    }
}