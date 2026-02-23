namespace GroceryStore.Features.Admin.Product.GetProducts;

public record GetProductsResponse(
    int Id,
    string Name,
    decimal Price,
    int CategoryId,
    string Sku,
    string? Description,
    string BaseUnit,
    List<GetProductsMetadataResponse> Metadata);

public record GetProductsMetadataResponse(
        string? Name,
        string? Value,
        string? Unit);

public sealed record ProductRow(
    int Id,
    string Name,
    decimal Price,
    int CategoryId,
    string Sku,
    string? Description,
    string BaseUnit,
    string? AttributeName,
    string? AttributeValue,
    string? AttributeUnit);