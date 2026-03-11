namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using GroceryStore.Shared.Models.Optional;

public record UpdateProductRequest(
    Optional<string?> NewName,
    Optional<string?> NewDescription,
    Optional<decimal?> NewPrice,
    Optional<int?> NewCategoryId,
    Optional<string?> NewBaseUnit);