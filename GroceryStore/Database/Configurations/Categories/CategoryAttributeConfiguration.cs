namespace GroceryStore.Database.Configurations.CategoryAttributes;

using Entities.CategoryAttribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryAttributeConfiguration : IEntityTypeConfiguration<CategoryAttribute>
{
    public void Configure(EntityTypeBuilder<CategoryAttribute> builder)
    {
        builder.HasKey(ca => new { ca.CategoryId, ca.AttributeId });
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.AttributeId).IsRequired();

        builder.HasOne(ca => ca.Category)
            .WithMany(c => c.Attributes)
            .HasForeignKey(ca => ca.CategoryId);

        builder.HasOne(ca => ca.Attribute)
            .WithMany()
            .HasForeignKey(ca => ca.AttributeId);

        builder.HasData(
            // ========== DAIRY PRODUCTS ==========
            new { CategoryId = 1, AttributeId = 1, IsRequired = true }, // Fat Content
            new { CategoryId = 1, AttributeId = 8, IsRequired = true }, // Lactose Free
            new { CategoryId = 1, AttributeId = 22, IsRequired = true }, // Shelf Life

            // Milk (12)
            new { CategoryId = 12, AttributeId = 3, IsRequired = true }, // Volume
            new { CategoryId = 12, AttributeId = 4, IsRequired = true }, // Milk Source

            // Plant-Based Milk (18)
            new { CategoryId = 18, AttributeId = 5, IsRequired = true }, // Base Ingredient

            // Drinkable Fermented (13)
            new { CategoryId = 13, AttributeId = 3, IsRequired = true }, // Volume
            new { CategoryId = 20, AttributeId = 6, IsRequired = false }, // Flavor
            new { CategoryId = 20, AttributeId = 9, IsRequired = true }, // Sugar Content

            // Spoonable Dairy (14)
            new { CategoryId = 14, AttributeId = 2, IsRequired = true }, // Weight
            new { CategoryId = 14, AttributeId = 6, IsRequired = false }, // Flavor
            new { CategoryId = 14, AttributeId = 9, IsRequired = true }, // Sugar Content
            new { CategoryId = 14, AttributeId = 10, IsRequired = false }, // Contains Filling

            // Butter (16)
            new { CategoryId = 16, AttributeId = 2, IsRequired = true }, // Weight

            // Cheese (15)
            new { CategoryId = 15, AttributeId = 2, IsRequired = true }, // Weight
            new { CategoryId = 15, AttributeId = 4, IsRequired = true }, // Milk Source
            new { CategoryId = 23, AttributeId = 7, IsRequired = true }, // Mold Type

            // ========== MEAT & POULTRY ==========
            new { CategoryId = 3, AttributeId = 2, IsRequired = true }, // Weight
            new { CategoryId = 3, AttributeId = 16, IsRequired = true }, // Temperature Storage
            new { CategoryId = 3, AttributeId = 20, IsRequired = true }, // Processing Method
            new { CategoryId = 3, AttributeId = 23, IsRequired = true }, // Protein Content

            // Beef (34)
            new { CategoryId = 34, AttributeId = 18, IsRequired = true }, // Meat Part
            new { CategoryId = 34, AttributeId = 17, IsRequired = false }, // Cut Type

            // Pork (35)
            new { CategoryId = 35, AttributeId = 18, IsRequired = true }, // Meat Part
            new { CategoryId = 35, AttributeId = 17, IsRequired = false }, // Cut Type

            // Poultry (36)
            new { CategoryId = 36, AttributeId = 18, IsRequired = true }, // Meat Part

            // Lamb (37)
            new { CategoryId = 37, AttributeId = 18, IsRequired = true }, // Meat Part

            // Ground Meat (38)
            new { CategoryId = 38, AttributeId = 1, IsRequired = true }, // Fat Content

            // ========== FISH & SEAFOOD ==========
            new { CategoryId = 4, AttributeId = 2, IsRequired = true }, // Weight
            new { CategoryId = 4, AttributeId = 16, IsRequired = true }, // Temperature Storage
            new { CategoryId = 4, AttributeId = 23, IsRequired = true }, // Protein Content

            // Fresh Fish (39)
            new { CategoryId = 39, AttributeId = 19, IsRequired = true }, // Fish Type

            // Frozen Fish (40)
            new { CategoryId = 40, AttributeId = 19, IsRequired = true }, // Fish Type

            // Seafood (41)
            new { CategoryId = 41, AttributeId = 20, IsRequired = true }, // Processing Method

            // ========== BAKERY & PASTRY ==========
            new { CategoryId = 5, AttributeId = 2, IsRequired = true }, // Weight
            new { CategoryId = 5, AttributeId = 22, IsRequired = true }, // Shelf Life
            new { CategoryId = 5, AttributeId = 26, IsRequired = false }, // Gluten Free

            // Bread (43)
            new { CategoryId = 43, AttributeId = 21, IsRequired = true }, // Grain Type

            // Pastries (44)
            new { CategoryId = 44, AttributeId = 9, IsRequired = true }, // Sugar Content
            new { CategoryId = 44, AttributeId = 10, IsRequired = false }, // Contains Filling

            // Cakes & Desserts (45)
            new { CategoryId = 45, AttributeId = 9, IsRequired = true }, // Sugar Content

            // ========== FRUITS & VEGETABLES ==========
            new { CategoryId = 7, AttributeId = 2, IsRequired = true }, // Weight
            new { CategoryId = 7, AttributeId = 13, IsRequired = false }, // Organic

            // Vegetables (46)
            new { CategoryId = 46, AttributeId = 25, IsRequired = false }, // Variety

            // Fruits (47)
            new { CategoryId = 47, AttributeId = 25, IsRequired = false }, // Variety
            new { CategoryId = 47, AttributeId = 24, IsRequired = false }, // Sweetness Level

            // Berries (48)
            new { CategoryId = 48, AttributeId = 25, IsRequired = false }, // Variety

            // ========== GROCERY ==========
            new { CategoryId = 6, AttributeId = 2, IsRequired = true }, // Weight

            // ========== BEVERAGES ==========
            new { CategoryId = 8, AttributeId = 3, IsRequired = true }, // Volume
            new { CategoryId = 8, AttributeId = 22, IsRequired = true }, // Shelf Life

            // Soft Drinks (50)
            new { CategoryId = 50, AttributeId = 9, IsRequired = true }, // Sugar Content
            new { CategoryId = 50, AttributeId = 29, IsRequired = true }, // Carbonation

            // Juices (51)
            new { CategoryId = 51, AttributeId = 9, IsRequired = true }, // Sugar Content
            new { CategoryId = 51, AttributeId = 6, IsRequired = false }, // Flavor

            // Water (52)
            new { CategoryId = 52, AttributeId = 29, IsRequired = true }, // Carbonation

            // Tea & Coffee (53)
            new { CategoryId = 53, AttributeId = 28, IsRequired = false }, // Caffeine Content

            // ========== ALCOHOL ==========
            new { CategoryId = 9, AttributeId = 11, IsRequired = true }, // Alcohol Content
            new { CategoryId = 9, AttributeId = 3, IsRequired = true }, // Volume

            // Beer (26)
            new { CategoryId = 26, AttributeId = 6, IsRequired = false }, // Flavor

            // ========== HOUSEHOLD CHEMICALS ==========
            new { CategoryId = 10, AttributeId = 3, IsRequired = true }, // Volume
            new { CategoryId = 10, AttributeId = 30, IsRequired = true }, // Chemical Type

            // Cleaning Products (54)
            new { CategoryId = 54, AttributeId = 27, IsRequired = false }, // Packaging Type

            // Laundry Detergents (55)
            new { CategoryId = 55, AttributeId = 27, IsRequired = false }, // Packaging Type

            // ========== PERSONAL CARE ==========
            new { CategoryId = 11, AttributeId = 3, IsRequired = true }, // Volume

            // Shampoos & Conditioners (57)
            new { CategoryId = 57, AttributeId = 6, IsRequired = false }, // Flavor

            // Body Care (58)
            new { CategoryId = 58, AttributeId = 13, IsRequired = false } // Organic
        );
    }
}