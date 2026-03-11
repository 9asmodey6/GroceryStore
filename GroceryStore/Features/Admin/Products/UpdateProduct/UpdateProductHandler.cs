namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using GroceryStore.Database.Entities.Product;
using GroceryStore.Shared.Extensions;

public class UpdateProductHandler
{
    public void Apply(Product product, UpdateProductRequest request)
    {
        product.UpdateIfHasValue(request.NewName, (p, v) => p.SetName(v));
        product.UpdateIfHasValue(request.NewPrice, (p, v) => p.UpdatePrice(v));
        product.UpdateIfHasValue(request.NewDescription, (p, v) => p.UpdateDescription(v));
        product.UpdateIfHasValue(request.NewCategoryId, (p, v) => p.SetCategoryId(v));
        product.UpdateIfHasValue(request.NewBrandId, (p, v) => p.SetBrandId(v));
        product.UpdateIfHasValue(request.NewCountryId, (p, v) => p.SetCountryId(v));
        product.UpdateIfHasValue(request.NewBaseUnit, (p, v) => p.SetBaseUnit(v));
    }
}