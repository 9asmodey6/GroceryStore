namespace GroceryStore.Features.Admin.Product.GetProductById;

using Database.Entities.Product;

public record GetProductByIdResponse(
    int Id,
    string Name,
    decimal Price,
    string CategoryName,
    string BrandName,
    string CountryCode,
    string Sku,
    string? Description,
    string BaseUnit,
    List<GetProductByIdMetadataResponse> Metadata);

public record GetProductByIdMetadataResponse(
    string? Name,
    string? Value,
    string? Unit);