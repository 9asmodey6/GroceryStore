namespace GroceryStore.Features.Admin.Product.UpdateProduct;

using Shared.Models;

public record UpdateProductRequest(
    Optional<string?> NewName,
    Optional<string?> NewDescription,
    Optional<decimal?> NewPrice,
    Optional<int?> NewCategoryId,
    Optional<string?> NewBaseUnit);