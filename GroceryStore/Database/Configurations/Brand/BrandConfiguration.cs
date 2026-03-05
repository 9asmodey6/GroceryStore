namespace GroceryStore.Database.Configurations.Brand;

using GroceryStore.Database.Entities.Brand;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasData(
            new { Id = 1, Name = "Generic" },
            new { Id = 2, Name = "Coca-Cola" },
            new { Id = 3, Name = "Pepsi" }
        );
    }
}