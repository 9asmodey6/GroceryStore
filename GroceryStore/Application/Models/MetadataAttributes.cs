namespace GroceryStore.Application.Models;

using Domain.Enums;

public record MetadataAttributes(
    int AttributeId,
    string Name,
    AttributeDataType DataType,
    string? Unit,
    decimal? MinValue,
    decimal? MaxValue,
    bool IsRequired);