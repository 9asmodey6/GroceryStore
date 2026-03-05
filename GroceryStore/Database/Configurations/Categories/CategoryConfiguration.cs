namespace GroceryStore.Database.Configurations.Categories;

using Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        builder.HasMany(c => c.Attributes)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);

        // Categories
        builder.HasData(
            new { Id = 1, Name = "Dairy Products", ParentId = (int?)null },
            new { Id = 2, Name = "Eggs", ParentId = (int?)null },
            new { Id = 3, Name = "Meat & Poultry", ParentId = (int?)null },
            new { Id = 4, Name = "Fish & Seafood", ParentId = (int?)null },
            new { Id = 5, Name = "Bakery & Pastry", ParentId = (int?)null },
            new { Id = 6, Name = "Grocery", ParentId = (int?)null },
            new { Id = 7, Name = "Vegetables & Fruit", ParentId = (int?)null },
            new { Id = 8, Name = "Beverages", ParentId = (int?)null },
            new { Id = 9, Name = "Alcohol", ParentId = (int?)null },
            new { Id = 10, Name = "Household Chemicals", ParentId = (int?)null },
            new { Id = 11, Name = "Personal Care & Hygiene", ParentId = (int?)null },

            // Dairy
            new { Id = 12, Name = "Milk", ParentId = (int?)1 },
            new { Id = 13, Name = "Drinkable Fermented", ParentId = (int?)1 },
            new { Id = 14, Name = "Spoonable Dairy & Sour Cream", ParentId = (int?)1 },
            new { Id = 15, Name = "Cheese & Cottage Cheese", ParentId = (int?)1 },
            new { Id = 16, Name = "Butter", ParentId = (int?)1 },

            // Milk
            new { Id = 17, Name = "Animal Milk", ParentId = (int?)12 },
            new { Id = 18, Name = "Plant-Based Milk", ParentId = (int?)12 },

            // Fermented
            new { Id = 19, Name = "Traditional Kefir", ParentId = (int?)13 },
            new { Id = 20, Name = "Drinkable Yogurt", ParentId = (int?)13 },

            // Cheese
            new { Id = 21, Name = "Hard Cheese", ParentId = (int?)15 },
            new { Id = 22, Name = "Soft & Brine Cheese", ParentId = (int?)15 },
            new { Id = 23, Name = "Mold Cheese", ParentId = (int?)15 },

            // Alcohol hierarchy
            new { Id = 24, Name = "Low Alcohol", ParentId = (int?)9 },
            new { Id = 25, Name = "Strong Alcohol", ParentId = (int?)9 },
            new { Id = 26, Name = "Beer", ParentId = (int?)24 },
            new { Id = 27, Name = "Cider", ParentId = (int?)24 },
            new { Id = 28, Name = "Vodka", ParentId = (int?)25 },
            new { Id = 29, Name = "Whiskey", ParentId = (int?)25 },
            new { Id = 30, Name = "Cognac", ParentId = (int?)25 },
            new { Id = 31, Name = "Rum", ParentId = (int?)25 },
            new { Id = 32, Name = "Tequila", ParentId = (int?)25 },
            new { Id = 33, Name = "Gin", ParentId = (int?)25 }
        );
    }
}