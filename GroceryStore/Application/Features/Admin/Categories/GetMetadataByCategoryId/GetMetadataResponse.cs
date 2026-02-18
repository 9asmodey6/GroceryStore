namespace GroceryStore.Application.Features.Admin.Categories.GetMetadataByCategoryId;

using GroceryStore.Domain.Enums;

public record GetMetadataResponse(
    int AttributeId,
    string Name,
    string DataType,
    string? Unit,
    decimal? MinValue,
    decimal? MaxValue,
    bool IsRequired);