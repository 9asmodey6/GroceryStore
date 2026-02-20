namespace GroceryStore.Infrastructure.Persistence;

using Domain.Entities.CategoryAttribute;
using Domain.Entities.ProductBatch;
using GroceryStore.Domain.Entities.Attribute;
using GroceryStore.Domain.Entities.Category;
using GroceryStore.Domain.Entities.Product;
using GroceryStore.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductAttribute> Attributes { get; set; }

    public DbSet<ProductBatch> ProductBatches { get; set; }

    public DbSet<CategoryAttribute> CategoryAttributes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.AddPostgreSqlRules();
        modelBuilder.OnDeleteRestrictRules();
    }
}