namespace GroceryStore.Application.Models;

using Domain.Enums;

public record MetadataAttributes(
    int AttributeId,
    string Name,
    AttributeDataType DataType,
    string? Unit,
    bool IsRequired,
    decimal? MinValue,
    decimal? MaxValue);