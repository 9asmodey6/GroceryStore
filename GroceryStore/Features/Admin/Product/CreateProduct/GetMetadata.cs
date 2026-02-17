using Dapper;
using Microsoft.Extensions.Caching.Memory;
using GroceryStore.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Features.Admin.Product.CreateProduct;

public class GetMetadataEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/admin/categories/{categoryId:int:min(1)}/metadata", HandleAsync)
            .WithTags("AdminCategories")
            .WithSummary("Get category attributes metadata (recursive)")
            .WithName("GetCategoryMetadata")
            .Produces<List<AttributeDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> HandleAsync(
        int categoryId,
        GetMetadataRepository repository,
        IMemoryCache cache)
    {
        const string cacheKeyPrefix = "category_metadata";

        var cacheKey = $"{cacheKeyPrefix}_{categoryId}";

        if (cache.TryGetValue(cacheKey, out List<AttributeDTO>? cachedMetadata) && cachedMetadata != null)
        {
            return Results.Ok(cachedMetadata);
        }

        var metadata = await repository.GetAttributesResponseAsync(categoryId);
        cache.Set(cacheKey, metadata, TimeSpan.FromMinutes(30));
        return Results.Ok(metadata);
    }
}