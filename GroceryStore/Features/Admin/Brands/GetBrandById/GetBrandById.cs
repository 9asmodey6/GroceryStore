namespace GroceryStore.Features.Admin.Brands.GetBrandById;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Interfaces;

public class GetBrandById : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/brands/{brandId:int:min(1)}", HandleAsync)
            .WithTags("AdminBrands")
            .WithSummary("Get Brand by ID")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Results<Ok<GetBrandByIdResponse>, NotFound>> HandleAsync(
        int brandId,
        GetBrandByIdRepository repository,
        CancellationToken ct)
    {
        var result = await repository.GetAsync(brandId, ct);

        if (result == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(result);
    }
}