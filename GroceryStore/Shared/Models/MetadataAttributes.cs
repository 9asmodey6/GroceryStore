namespace GroceryStore.Shared.Models;

using GroceryStore.Database.Enums;

public record MetadataAttributes(
    int AttributeId,
    string Name,
    AttributeDataType DataType,
    string? Unit,
    decimal? MinValue,
    decimal? MaxValue,
    bool IsRequired);