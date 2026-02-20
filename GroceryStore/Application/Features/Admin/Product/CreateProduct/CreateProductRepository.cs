namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using Domain.Entities.Product;
using Infrastructure.Persistence;

public class CreateProductRepository
{
    private readonly AppDbContext _dbContext;

    public CreateProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Product product, CancellationToken ct)
    {
        await _dbContext.Products.AddAsync(product, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}