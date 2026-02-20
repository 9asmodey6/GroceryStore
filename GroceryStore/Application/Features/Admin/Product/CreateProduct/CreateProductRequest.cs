namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using Models;

public record CreateProductRequest(
    string Name,
    int CategoryId,
    string? Description,
    decimal Price,
    string? BaseUnit,
    List<EnumerationModel> Attributes);