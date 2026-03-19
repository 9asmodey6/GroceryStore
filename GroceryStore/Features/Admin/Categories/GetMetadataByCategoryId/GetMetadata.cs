namespace GroceryStore.Features.Admin.Categories.GetMetadataByCategoryId;

using Infrastructure.Repositories.Categories;
using Microsoft.Extensions.Caching.Memory;
using Shared.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Models;

public class GetMetadataEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/admin/categories/{categoryId:int:min(1)}/metadata", HandleAsync)
            .WithTags("AdminCategories")
            .WithSummary("Get category attributes metadata (recursive)")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Ok<List<MetadataAttribute>>> HandleAsync(
        int categoryId,
        CategoryAttributeRepository repository,
        IMemoryCache cache,
        CancellationToken ct)
    {
        var cachePrefix = CategoryCacheKeys.CategoryMetadata;

        string cacheKey = $"{cachePrefix}_{categoryId}";

        if (cache.TryGetValue(cacheKey, out List<MetadataAttribute>? cachedMetadata) && cachedMetadata != null)
        {
            return TypedResults.Ok(cachedMetadata);
        }

        var metadata = await repository.GetMetadataSchemaAsync(categoryId, ct);
        cache.Set(cacheKey, metadata, TimeSpan.FromMinutes(30));
        return TypedResults.Ok(metadata);
    }
}