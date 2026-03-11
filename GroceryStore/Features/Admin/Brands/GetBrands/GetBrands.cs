namespace GroceryStore.Features.Admin.Brands.GetBrands;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class GetBrands : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/brands", HandleAsync)
            .WithTags("AdminBrands")
            .WithSummary("Get All Brands")
            .WithName("GetBrands");
    }

    public static async Task<Results<Ok<List<GetBrandsResponse>>, ForbidHttpResult>> HandleAsync(
        CancellationToken ct,
        GetBrandsRepository repository)
    {
        var brands = await repository.GetBrandsAsync(ct);
        return TypedResults.Ok(brands);
    }
}