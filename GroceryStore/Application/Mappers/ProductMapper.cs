namespace GroceryStore.Application.Mappers;

using Domain.Entities.Product;
using Features.Admin.Product.CreateProduct;

public static class ProductMapper
{
    public static Product ToEntity(
        CreateProductRequest request,
        string sku,
        Dictionary<int, string> normalized)
    {
        return new Product(
            request.Name,
            request.Price,
            request.CategoryId,
            sku,
            request?.Description,
            request?.BaseUnit,
            normalized);
    }
}