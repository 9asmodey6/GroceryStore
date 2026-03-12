namespace GroceryStore.Features.Admin.Brands.DeleteBrand;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;
using Shared.Interfaces;

public class DeleteBrand : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/admin/brands/{brandId:int:min(1)}", HandleAsync)
            .WithTags("AdminBrands")
            .WithSummary("Delete Brand By ID")
            .WithName("DeleteBrand");
    }

    private static async Task<Results<NoContent, NotFound>> HandleAsync(
        CancellationToken ct,
        DeleteBrandRepository repository,
        IMemoryCache cache,
        int brandId)
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