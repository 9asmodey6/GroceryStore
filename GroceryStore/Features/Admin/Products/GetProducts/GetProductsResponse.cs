namespace GroceryStore.Features.Admin.Products.GetProducts;

public record GetProductsResponse(
    GetProductsResponseItem[] Items);

public record GetProductsResponseItem(
    int Id,
    string Name,
    decimal Price,
    string CategoryName,
    string BrandName,
    string CountryCode,
    string Sku,
    string? Description,
    string BaseUnit,
    List<GetProductsMetadataResponse> Metadata);

public record GetProductsMetadataResponse(
        string? Name,
        string? Value,
        string? Unit);