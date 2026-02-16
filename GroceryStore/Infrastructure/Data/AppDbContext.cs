using GroceryStore.Database.Configurations.Attributes;
using GroceryStore.Database.Extensions;

namespace GroceryStore.Infrastructure;

using Microsoft.EntityFrameworkCore;
using GroceryStore.Domain;

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