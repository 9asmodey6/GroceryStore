namespace GroceryStore.Application.Features.Admin.Categories.GetMetadataByCategoryId;

using GroceryStore.Domain.Enums;

public record GetMetadataResponse(
    int AttributeId,
    string Name,
    AttributeDataType DataType,
    string? Unit,
    bool IsRequired,
    decimal? MinValue,
    decimal? MaxValue);