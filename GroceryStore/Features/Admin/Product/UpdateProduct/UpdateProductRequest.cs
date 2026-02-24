namespace GroceryStore.Features.Admin.Product.UpdateProduct;

using Shared.Models;

public record UpdateProductRequest(
    string? NewName,
    string? NewDescription,
    int? NewPrice,
    int? NewCategoryId,
    string? NewBaseUnit,
    List<EnumerationModel>? Metadata);