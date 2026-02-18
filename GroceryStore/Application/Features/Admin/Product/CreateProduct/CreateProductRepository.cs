namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using Domain.Entities.Product;
using Infrastructure.Persistence;

public class CreateProductRepository
{
    public void CreateAsync(Product product, AppDbContext context)
    {
       context.AddAsync(product);
    }
}