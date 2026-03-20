namespace GroceryStore.Infrastructure.Services;

using System.Globalization;
using Database.Enums;
using Repositories.Categories;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;
using Shared.Consts.CacheKeys;
using Shared.Enums;
using Shared.Models;
using Shared.Models.Results;

public class CategoryAttributeValueNormalizer(IMemoryCache cache, CategoryAttributeRepository repository)
{
    public async Task<NormalizationResult<Dictionary<int, string>>> ValidateAndNormalizeAsync(
        int categoryId,
        IReadOnlyList<EnumerationModel> values,
        CancellationToken ct)
    {
        var vr = new ValidationResult();

        var metadata = await GetMetadataAsync(categoryId, ct);
        if (metadata.Count == 0)
        {
            vr.Add(
                MetadataValidationErrorCode.NotFoundMetadata,
                "No metadata for category",
                field: $"categoryId={categoryId}");
            return NormalizationResult<Dictionary<int, string>>.Fail(vr);
        }

        var metaById = metadata.ToDictionary(x => x.AttributeId);

        // 1) unknown ids
        for (int i = 0; i < values.Count; i++)
        {
            var item = values[i];

            if (!metaById.ContainsKey(item.Id))
            {
                vr.Add(
                    MetadataValidationErrorCode.UnknowAttribute,
                    $"AttributeId={item.Id} is not allowed for this category",
                    $"attributes[{i}].id",
                    item.Id);
            }
        }

        // 2) required present
        var provided = values.Select(v => v.Id).ToHashSet();
        foreach (var m in metadata.Where(m => m.IsRequired))
        {
            if (!provided.Contains(m.AttributeId))
            {
                vr.Add(
                    MetadataValidationErrorCode.Required,
                    $"Required attribute is missing: {m.Name} (id = {m.AttributeId})",
                    valueId: m.AttributeId);
            }
        }

        var normalized = new Dictionary<int, string>();

        // validate by type and return normalized
        for (int i = 0; i < values.Count; i++)
        {
            var item = values[i];

            if (!metaById.TryGetValue(item.Id, out var meta))
            {
                continue;
            }

            var field = $"attributes[{i}].value";

            var perItem = ValidateAndNormalizeSingle(item.Value, meta, field);

            vr.Merge(perItem.Validation);

            if (perItem.IsSuccess && perItem.Value != null)
            {
                normalized[item.Id] = perItem.Value;
            }
        }

        var duplicates = values
            .GroupBy(v => v.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        foreach (var d in duplicates)
        {
            vr.Add(
                MetadataValidationErrorCode.Required,
                $"Duplicate attributeId = {d} in request",
                "attributes",
                d);
        }

        return !vr.IsValid
            ? NormalizationResult<Dictionary<int, string>>.Fail(vr)
            : NormalizationResult<Dictionary<int, string>>.Success(normalized);
    }

    private async ValueTask<List<MetadataAttribute>> GetMetadataAsync(int categoryId, CancellationToken ct)
    {
        const string cacheKeyPrefix = CategoryCacheKeys.CategoryMetadata;

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


    private NormalizationResult<string> ValidateAndNormalizeSingle(
        string raw,
        MetadataAttribute meta,
        string field)
    {
        var vr = new ValidationResult();

        if (string.IsNullOrWhiteSpace(raw))
        {
            if (meta.IsRequired)
            {
                vr.Add(
                    MetadataValidationErrorCode.Required,
                    $"{meta.Name} is required,",
                    field,
                    meta.AttributeId);
                return NormalizationResult<string>.Fail(vr);
            }

            return NormalizationResult<string>.Success(null!);
        }

        var trimed = raw.Trim();

        switch (meta.DataType)
        {
            case AttributeDataType.Integer:
            {
                if (!TryParseInt(trimed, out var number))
                {
                    vr.Add(
                        MetadataValidationErrorCode.InvalidType,
                        $"{meta.Name} must be an integer",
                        field,
                        meta.AttributeId);
                    return NormalizationResult<string>.Fail(vr);
                }

                if (!InRange(number, meta))
                {
                    vr.Add(
                        MetadataValidationErrorCode.OutOfRange,
                        $"{meta.Name} is out of range",
                        field,
                        meta.AttributeId);
                    return NormalizationResult<string>.Fail(vr);
                }

                return NormalizationResult<string>.Success(number.ToString(
                    CultureInfo.InvariantCulture));
            }

            case AttributeDataType.Decimal:
            {
                if (!TryParseDecimal(trimed, out var number))
                {
                    vr.Add(
                        MetadataValidationErrorCode.InvalidType,
                        $"{meta.Name} must be a decimal",
                        field,
                        meta.AttributeId);
                    return NormalizationResult<string>.Fail(vr);
                }

                if (!InRange(number, meta))
                {
                    vr.Add(
                        MetadataValidationErrorCode.OutOfRange,
                        $"{meta.Name} is out of range",
                        field,
                        meta.AttributeId);
                    return NormalizationResult<string>.Fail(vr);
                }

                var normalized = NormalizeDecimal(number);

                return NormalizationResult<string>.Success(normalized!);
            }

            case AttributeDataType.Boolean:
            {
                if (!TryParseBoolean(trimed, out var result))
                {
                    vr.Add(
                        MetadataValidationErrorCode.InvalidType,
                        $"{meta.Name} must contain * true / false / 1 / 0 * only",
                        field,
                        meta.AttributeId);
                    return NormalizationResult<string>.Fail(vr);
                }

                var normalized = NormalizeBoolean(result);

                return NormalizationResult<string>.Success(normalized);
            }

            case AttributeDataType.String:
            {
                return NormalizationResult<string>.Success(trimed);
            }

            case AttributeDataType.DateTime:
            {
                vr.Add(
                    MetadataValidationErrorCode.InvalidType,
                    $"{meta.Name} DateTime validation is not implemented yet",
                    field,
                    meta.AttributeId);
                return NormalizationResult<string>.Fail(vr);
            }

            default:
            {
                vr.Add(
                    MetadataValidationErrorCode.InvalidType,
                    $"{meta.Name} has unsupported data type",
                    field,
                    meta.AttributeId);
                return NormalizationResult<string>.Fail(vr);
            }
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

    private static bool TryParseInt(string input, out int value)
    {
        value = 0;

        return int.TryParse(
            input,
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out value);
    }

    private static bool TryParseDecimal(string input, out decimal value)
    {
        value = 0;

        return decimal.TryParse(
            input.Replace(',', '.').Trim(),
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out value);
    }

    private static string? NormalizeDecimal(decimal input)
    {
        var rounded = Math.Round(input, 2, MidpointRounding.AwayFromZero);

        return rounded.ToString(CultureInfo.InvariantCulture);
    }

    private static bool TryParseBoolean(string input, out bool value)
    {
        input = input.Trim();

        if (input == "1")
        {
            value = true;
            return true;
        }

        if (input == "0")
        {
            value = false;
            return false;
        }

        return bool.TryParse(input, out value);
    }

    private static string NormalizeBoolean(bool value)
    {
        return value ? "true" : "false";
    }
}