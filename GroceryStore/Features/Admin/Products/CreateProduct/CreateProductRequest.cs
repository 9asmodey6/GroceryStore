namespace GroceryStore.Features.Admin.Products.CreateProduct;

using GroceryStore.Shared.Models;

public record CreateProductRequest(
    string Name,
    int CategoryId,
    int BrandId,
    int CountryId,
    string? Description,
    decimal Price,
    string? BaseUnit,
    List<EnumerationModel> Attributes);