namespace GroceryStore.Database;

using Entities.Brand;
using Entities.Country;
using GroceryStore.Database.Entities.Attribute;
using GroceryStore.Database.Entities.Category;
using GroceryStore.Database.Entities.CategoryAttribute;
using GroceryStore.Database.Entities.Product;
using GroceryStore.Database.Entities.ProductBatch;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Brand> Brands { get; set; }

    public DbSet<Country> Countries { get; set; }

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