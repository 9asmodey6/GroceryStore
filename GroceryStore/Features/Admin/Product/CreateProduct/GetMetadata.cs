using Dapper;
using Microsoft.Extensions.Caching.Memory;
using GroceryStore.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Features.Admin.Product.CreateProduct;

public class GetMetadataEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/admin/products/metadata/{categoryId:int:min(1)}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Get attributes for category (recursive)");
    }

    private static async Task<IResult> HandleAsync(
        int categoryId,
        GetMetadataRepository repository,
        IMemoryCache cache)
    {
        var cacheKey = $"category_metadata_{categoryId}";

        if (cache.TryGetValue(cacheKey, out List<AttributeDTO>? cachedMetadata) && cachedMetadata != null)
            return Results.Ok(cachedMetadata);

        var metadata = await repository.GetAttributesResponseAsync(categoryId);
        cache.Set(cacheKey, metadata, TimeSpan.FromMinutes(30));
        return Results.Ok(metadata);
    }
}

public record AttributeDTO(string Name, string Unit, bool isRequired);