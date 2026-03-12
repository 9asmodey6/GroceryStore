namespace GroceryStore.Features.Admin.Countries.GetCountries;

using Brands.GetBrands;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class GetCountries : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/countries", HandleAsync)
            .WithTags("AdminCountries")
            .WithSummary("Get All Countries")
            .WithName("GetCountries");
    }

    private static async Task<Results<Ok<List<GetCountriesResponse>>, ForbidHttpResult>> HandleAsync(
        CancellationToken ct,
        GetCountriesRepository repository)
    {
        var countries = await repository.GetCountriesAsync(ct);
        return TypedResults.Ok(countries);
    }
}