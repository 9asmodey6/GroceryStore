namespace GroceryStore.Shared.Models;

using GroceryStore.Database.Enums;

public record MetadataAttribute(
    int AttributeId,
    string Name,
    AttributeDataType DataType,
    string? Unit,
    decimal? MinValue,
    decimal? MaxValue,
    bool IsRequired);