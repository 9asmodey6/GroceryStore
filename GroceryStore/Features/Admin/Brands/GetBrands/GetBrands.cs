namespace GroceryStore.Features.Admin.Brands.GetBrands;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Interfaces;

public class GetBrands : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/brands", HandleAsync)
            .WithTags("AdminBrands")
            .WithSummary("Get All Brands")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Ok<GetBrandsResponse>> HandleAsync(
        GetBrandsRepository repository,
        CancellationToken ct)
    {
        var brands = await repository.GetBrandsAsync(ct);
        return TypedResults.Ok(brands);
    }
}