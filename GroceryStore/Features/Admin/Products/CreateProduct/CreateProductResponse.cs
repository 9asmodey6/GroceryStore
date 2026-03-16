namespace GroceryStore.Features.Admin.Products.CreateProduct;

public record CreateProductResponse(
    int Id,
    string Name,
    decimal Price,
    string Sku,
    string? Description,
    string? BaseUnit);