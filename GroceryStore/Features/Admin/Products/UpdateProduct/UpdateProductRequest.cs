namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using GroceryStore.Shared.Models.Optional;

public record UpdateProductRequest(
    Optional<string?> NewName,
    Optional<string?> NewDescription,
    Optional<decimal?> NewPrice,
    Optional<int?> NewCategoryId,
    Optional<int?> NewBrandId,
    Optional<int> NewCountryId,
    Optional<string?> NewBaseUnit);