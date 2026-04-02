namespace GroceryStore.Features.Admin.Products.CreateProduct;

using GroceryStore.Database;
using GroceryStore.Database.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;

public class CreateProductRepository(AppDbContext dbContext) : IRepository
{
    public async Task CreateAsync(Product product, CancellationToken ct)
    {
        dbContext.Add(product);
        await dbContext.SaveChangesAsync(ct);
    }
}