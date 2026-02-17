namespace GroceryStore.Features.Admin.Product.CreateProduct;

using Domain.Enums;

public record AttributeDTO(
    int AttributeId,
    string Name,
    AttributeDataType DataType,
    string? Unit,
    bool IsRequired,
    decimal? MinValue,
    decimal? MaxValue);

public record AttributeValueDTO(int AttributeId, string Value);

public record ProductRequestDto(
    string Name,
    int CategoryId,
    string? Description,
    decimal Price,
    string? BaseUnit,
    List<AttributeValueDTO> Attributes);