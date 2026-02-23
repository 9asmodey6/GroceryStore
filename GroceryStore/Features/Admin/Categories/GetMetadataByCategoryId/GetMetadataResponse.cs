namespace GroceryStore.Features.Admin.Categories.GetMetadataByCategoryId;

public record GetMetadataResponse(
    int AttributeId,
    string Name,
    string DataType,
    string? Unit,
    decimal? MinValue,
    decimal? MaxValue,
    bool IsRequired);