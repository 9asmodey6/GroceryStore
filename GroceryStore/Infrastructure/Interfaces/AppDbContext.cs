namespace GroceryStore.Infrastructure;

using Microsoft.EntityFrameworkCore;
using GroceryStore.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Attribute> Attributes => Set<Attribute>();

    public DbSet<StockBatch> StockBatches => Set<StockBatch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}