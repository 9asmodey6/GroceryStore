namespace GroceryStore.Features.Admin.Brands.GetBrands;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Extensions;
using Shared.Interfaces;

public class GetBrands : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/brands", HandleAsync)
            .WithTags(EndpointTags.AdminBrands)
            .RequireAdminRole()
            .WithSummary("Get All Brands")
            .RequireAdminRole()
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