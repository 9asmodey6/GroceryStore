namespace GroceryStore.Features.Admin.Products.UpdateProduct;

public record UpdateProductResponse(
    int Id,
    string Name,
    decimal Price,
    string Sku,
    string? Description,
    string? BaseUnit);