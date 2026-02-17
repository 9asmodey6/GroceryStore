namespace GroceryStore.Features.Admin.Product.CreateProduct;

using Microsoft.Extensions.Caching.Memory;

public class AttributeSchemaValidator
{
    private readonly IMemoryCache _cache;
    private readonly GetMetadataRepository _repository;

    public AttributeSchemaValidator(IMemoryCache cache, GetMetadataRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }

    public async Task<Dictionary<int, string>> ValidateAndNormalizeAsync(
        int categoryId,
        IReadOnlyList<AttributeValueDTO> values,
        CancellationToken ct)
    {
    }

    public async Task<List<AttributeDTO>> GetMetadataAsync(int categoryId, CancellationToken ct)
    {
        const string cacheKeyPrefix = "category_metadata";

        var cacheKey = $"{cacheKeyPrefix}_{categoryId}";
        return (await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var metadata = await _repository.GetAttributesResponseAsync(categoryId, ct);

            if (metadata.Count == 0)
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
            }

            return metadata;
        })) !;
    }
}