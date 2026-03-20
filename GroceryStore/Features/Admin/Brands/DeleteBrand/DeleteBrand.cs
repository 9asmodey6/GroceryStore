namespace GroceryStore.Features.Admin.Brands.DeleteBrand;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;
using Shared.Consts.CacheKeys;
using Shared.Consts.Endpoints;
using Shared.Extensions;
using Shared.Interfaces;

public class DeleteBrand : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/admin/brands/{brandId:int:min(1)}", HandleAsync)
            .WithTags(EndpointTags.AdminBrands)
            .RequireAdminRole()
            .WithSummary("Delete Brand by ID")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Results<NoContent, NotFound>> HandleAsync(
        DeleteBrandRepository repository,
        IMemoryCache cache,
        int brandId,
        CancellationToken ct)
    {
        var isDeleted = await repository.DeleteBrandAsync(brandId, ct);

        if (!isDeleted)
        {
            return TypedResults.NotFound();
        }

        cache.Remove(LookupCacheKeys.Brands);
        return TypedResults.NoContent();
    }
}