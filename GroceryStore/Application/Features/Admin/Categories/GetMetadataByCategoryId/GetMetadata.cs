namespace GroceryStore.Application.Features.Admin.Categories.GetMetadataByCategoryId;

using Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Models;
public class GetMetadataEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/admin/categories/{categoryId:int:min(1)}/metadata", HandleAsync)
            .WithTags("AdminCategories")
            .WithSummary("Get category attributes metadata (recursive)")
            .WithName("GetCategoryMetadata")
            .Produces<List<MetadataAttributes>>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> HandleAsync(
        int categoryId,
        GetMetadataRepository repository,
        IMemoryCache cache,
        CancellationToken ct)
    {
        const string cacheKeyPrefix = "category_metadata";

        var cacheKey = $"{cacheKeyPrefix}_{categoryId}";

        if (cache.TryGetValue(cacheKey, out List<GetMetadataResponse>? cachedMetadata) && cachedMetadata != null)
        {
            return Results.Ok(cachedMetadata);
        }

        var metadata = await repository.GetAttributesResponseAsync(categoryId, ct);
        cache.Set(cacheKey, metadata, TimeSpan.FromMinutes(30));
        return Results.Ok(metadata);
    }
}