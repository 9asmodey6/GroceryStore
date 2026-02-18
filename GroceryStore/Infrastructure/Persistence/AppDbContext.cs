namespace GroceryStore.Infrastructure.Persistence;

using GroceryStore.Domain.Entities.Attribute;
using GroceryStore.Domain.Entities.Category;
using GroceryStore.Domain.Entities.Product;
using GroceryStore.Domain.Entities.Stock;
using GroceryStore.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Attribute> Attributes { get; set; }

    public DbSet<StockBatch> StockBatches { get; set; }

    public DbSet<CategoryAttribute> CategoryAttributes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.AddPostgreSqlRules();
        modelBuilder.OnDeleteRestrictRules();
    }
}