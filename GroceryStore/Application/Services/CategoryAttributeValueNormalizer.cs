namespace GroceryStore.Application.Services;

using Microsoft.Extensions.Caching.Memory;
using Models;

public class CategoryAttributeValueNormalizer
{
    private readonly IMemoryCache _cache;
    private readonly CategoryAttributeService _service;

    public CategoryAttributeValueNormalizer(IMemoryCache cache, CategoryAttributeService service)
    {
        _cache = cache;
        _service = service;
    }

    public async Task<Dictionary<int, string>> ValidateAndNormalizeAsync(
        int categoryId,
        IReadOnlyList<AttributeValueDTO> values,
        CancellationToken ct)
    {
        var metadata = await GetMetadataAsync(categoryId, ct);
        if (metadata.Count == 0)
        {
            throw new ArgumentException("No metadata for category");
        }

        var metaById = metadata.ToDictionary(x => x.AttributeId);

        // 1) unknown ids
        foreach (var v in values)
        {
            if (!metaById.ContainsKey(v.AttributeId))
            {
                throw new ArgumentException($"AttributeId={v.AttributeId} not allowed");
            }
        }

        // 2) required present
        var provided = values.Select(v => v.AttributeId).ToHashSet();
        var missingRequired = metadata.Where(m => m.IsRequired && !provided.Contains(m.AttributeId)).ToList();
        if (missingRequired.Count > 0)
        {
            throw new ArgumentException("Missing required attributes");
        }

        // 3) normalize = Trim
        return values
            .Where(v => !string.IsNullOrWhiteSpace(v.Value))
            .ToDictionary(v => v.AttributeId, v => v.Value.Trim());
    }

    public async Task<List<MetadataAttributes>> GetMetadataAsync(int categoryId, CancellationToken ct)
    {
        const string cacheKeyPrefix = "category_metadata";

        var cacheKey = $"{cacheKeyPrefix}_{categoryId}";
        return (await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var metadata = await _service.GetMetadataSchemaAsync(categoryId, ct);

            if (metadata.Count == 0)
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
            }

            return metadata;
        })) !;
    }
}