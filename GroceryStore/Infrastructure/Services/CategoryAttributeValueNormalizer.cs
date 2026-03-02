namespace GroceryStore.Infrastructure.Services;

using System.Globalization;
using Repositories.Categories;
using Microsoft.Extensions.Caching.Memory;
using Shared.Models;

public class CategoryAttributeValueNormalizer(IMemoryCache cache, CategoryAttributeRepository repository)
{
    public async Task<Dictionary<int, string>> ValidateAndNormalizeAsync(
        int categoryId,
        IReadOnlyList<EnumerationModel> values,
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
            if (!metaById.ContainsKey(v.Id))
            {
                throw new ArgumentException($"AttributeId={v.Id} not allowed");
            }
        }

        // 2) required present
        var provided = values.Select(v => v.Id).ToHashSet();
        var missingRequired = metadata.Where(m => m.IsRequired && !provided.Contains(m.AttributeId))
            .Select(m => $"{m.AttributeId} ({m.Name})")
            .ToList();
        if (missingRequired.Count > 0)
        {
            throw new ArgumentException("Missing required attributes");
        }

        // 3) normalize = Trim
        return values
            .Where(v => !string.IsNullOrWhiteSpace(v.Value))
            .ToDictionary(v => v.Id, v => v.Value.Trim());
    }

    private async ValueTask<List<MetadataAttribute>> GetMetadataAsync(int categoryId, CancellationToken ct)
    {
        const string cacheKeyPrefix = "category_metadata";

        var cacheKey = $"{cacheKeyPrefix}_{categoryId}";
        return (await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var metadata = await repository.GetMetadataSchemaAsync(categoryId, ct);

            if (metadata.Count == 0)
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
            }

            return metadata;
        })) !;
    }


    private static bool ValidateAndNormalizeByType(IReadOnlyList<EnumerationModel> attributes, List<MetadataAttribute> metadata)
    {
        foreach (var a in attributes)
        {
            if()
        }
    }
    
    private static bool InRange(decimal value, MetadataAttribute attribute)
    {
        if (attribute.MinValue.HasValue && value < attribute.MinValue.Value)
        {
            return false;
        }

        if (attribute.MaxValue.HasValue && value > attribute.MaxValue.Value)
        {
            return false;
        }

        return true;
    }

    private bool ValidateInt(string value, MetadataAttribute attribute)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return !attribute.IsRequired;
        }

        if (!int.TryParse(
                value,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var number))
        {
            return false;
        }

        return InRange(number, attribute);
    }

    private bool ValidateDecimal(string value, MetadataAttribute attribute)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return !attribute.IsRequired;
        }

        var clean = value.Replace(',', '.').Trim();

        if (!decimal.TryParse(
                clean,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var number))
        {
            return false;
        }

        return InRange(number, attribute);
    }

    private string? NormalizeDecimal(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        var clean = input.Replace(',', '.').Trim();

        if (!decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
        {
            return null;
        }

        var rounded = Math.Round(value, 2, MidpointRounding.AwayFromZero);

        return rounded.ToString(CultureInfo.InvariantCulture);
    }

    public bool ValidateBoolean(string input, MetadataAttribute attribute)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return !attribute.IsRequired;
        }

        return true;
    }

    private string NormalizeBoolean(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (input == "1")
        {
            return "true";
        }

        if (input == "0")
        {
            return "false";
        }

        if (!bool.TryParse(input, out var result))
        {
            return null;
        }

        return result.ToString().ToLowerInvariant();
    }
}