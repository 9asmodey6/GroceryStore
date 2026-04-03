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
            // ========== УРОВЕНЬ 0: ROOT CATEGORIES ==========
            new { Id = 1, Name = "Dairy Products", ParentId = (int?)null },
            new { Id = 2, Name = "Eggs", ParentId = (int?)null },
            new { Id = 3, Name = "Meat & Poultry", ParentId = (int?)null },
            new { Id = 4, Name = "Fish & Seafood", ParentId = (int?)null },
            new { Id = 5, Name = "Bakery & Pastry", ParentId = (int?)null },
            new { Id = 6, Name = "Grocery", ParentId = (int?)null },
            new { Id = 7, Name = "Fruits & Vegetables", ParentId = (int?)null },
            new { Id = 8, Name = "Beverages", ParentId = (int?)null },
            new { Id = 9, Name = "Alcohol", ParentId = (int?)null },
            new { Id = 10, Name = "Household Chemicals", ParentId = (int?)null },
            new { Id = 11, Name = "Personal Care & Hygiene", ParentId = (int?)null },

            // ========== DAIRY PRODUCTS (1) ==========
            new { Id = 12, Name = "Milk", ParentId = (int?)1 },
            new { Id = 13, Name = "Drinkable Fermented", ParentId = (int?)1 },
            new { Id = 14, Name = "Spoonable Dairy & Sour Cream", ParentId = (int?)1 },
            new { Id = 15, Name = "Cheese & Cottage Cheese", ParentId = (int?)1 },
            new { Id = 16, Name = "Butter", ParentId = (int?)1 },

            // Milk (12)
            new { Id = 17, Name = "Animal Milk", ParentId = (int?)12 },
            new { Id = 18, Name = "Plant-Based Milk", ParentId = (int?)12 },

            // Fermented (13)
            new { Id = 19, Name = "Traditional Kefir", ParentId = (int?)13 },
            new { Id = 20, Name = "Drinkable Yogurt", ParentId = (int?)13 },

            // Cheese (15)
            new { Id = 21, Name = "Hard Cheese", ParentId = (int?)15 },
            new { Id = 22, Name = "Soft & Brine Cheese", ParentId = (int?)15 },
            new { Id = 23, Name = "Mold Cheese", ParentId = (int?)15 },

            // ========== MEAT & POULTRY (3) ==========
            new { Id = 34, Name = "Beef", ParentId = (int?)3 },
            new { Id = 35, Name = "Pork", ParentId = (int?)3 },
            new { Id = 36, Name = "Poultry", ParentId = (int?)3 },
            new { Id = 37, Name = "Lamb", ParentId = (int?)3 },
            new { Id = 38, Name = "Ground Meat", ParentId = (int?)3 },

            // ========== FISH & SEAFOOD (4) ==========
            new { Id = 39, Name = "Fresh Fish", ParentId = (int?)4 },
            new { Id = 40, Name = "Frozen Fish", ParentId = (int?)4 },
            new { Id = 41, Name = "Seafood", ParentId = (int?)4 },
            new { Id = 42, Name = "Canned Fish", ParentId = (int?)4 },

            // ========== BAKERY & PASTRY (5) ==========
            new { Id = 43, Name = "Bread", ParentId = (int?)5 },
            new { Id = 44, Name = "Pastries", ParentId = (int?)5 },
            new { Id = 45, Name = "Cakes & Desserts", ParentId = (int?)5 },

            // ========== FRUITS & VEGETABLES (7) ==========
            new { Id = 46, Name = "Vegetables", ParentId = (int?)7 },
            new { Id = 47, Name = "Fruits", ParentId = (int?)7 },
            new { Id = 48, Name = "Berries", ParentId = (int?)7 },
            new { Id = 49, Name = "Greens & Herbs", ParentId = (int?)7 },

            // ========== BEVERAGES (8) ==========
            new { Id = 50, Name = "Soft Drinks", ParentId = (int?)8 },
            new { Id = 51, Name = "Juices", ParentId = (int?)8 },
            new { Id = 52, Name = "Water", ParentId = (int?)8 },
            new { Id = 53, Name = "Tea & Coffee", ParentId = (int?)8 },

            // ========== ALCOHOL (9) ==========
            new { Id = 24, Name = "Low Alcohol", ParentId = (int?)9 },
            new { Id = 25, Name = "Strong Alcohol", ParentId = (int?)9 },
            new { Id = 26, Name = "Beer", ParentId = (int?)24 },
            new { Id = 27, Name = "Cider", ParentId = (int?)24 },
            new { Id = 28, Name = "Vodka", ParentId = (int?)25 },
            new { Id = 29, Name = "Whiskey", ParentId = (int?)25 },
            new { Id = 30, Name = "Cognac", ParentId = (int?)25 },
            new { Id = 31, Name = "Rum", ParentId = (int?)25 },
            new { Id = 32, Name = "Tequila", ParentId = (int?)25 },
            new { Id = 33, Name = "Gin", ParentId = (int?)25 },

            // ========== HOUSEHOLD CHEMICALS (10) ==========
            new { Id = 54, Name = "Cleaning Products", ParentId = (int?)10 },
            new { Id = 55, Name = "Laundry Detergents", ParentId = (int?)10 },
            new { Id = 56, Name = "Dishwashing", ParentId = (int?)10 },

            // ========== PERSONAL CARE (11) ==========
            new { Id = 57, Name = "Shampoos & Conditioners", ParentId = (int?)11 },
            new { Id = 58, Name = "Body Care", ParentId = (int?)11 },
            new { Id = 59, Name = "Oral Care", ParentId = (int?)11 }
        );
    }
}