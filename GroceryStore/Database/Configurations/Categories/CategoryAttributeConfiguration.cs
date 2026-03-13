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
            new { CategoryId = 1, AttributeId = 1, IsRequired = true },
            new { CategoryId = 1, AttributeId = 8, IsRequired = true },
            new { CategoryId = 12, AttributeId = 2, IsRequired = true },
            new { CategoryId = 12, AttributeId = 4, IsRequired = true },
            new { CategoryId = 18, AttributeId = 5, IsRequired = true },
            new { CategoryId = 13, AttributeId = 2, IsRequired = true },
            new { CategoryId = 20, AttributeId = 6, IsRequired = false },
            new { CategoryId = 20, AttributeId = 9, IsRequired = true },
            new { CategoryId = 14, AttributeId = 3, IsRequired = true },
            new { CategoryId = 14, AttributeId = 6, IsRequired = false },
            new { CategoryId = 14, AttributeId = 9, IsRequired = true },
            new { CategoryId = 14, AttributeId = 10, IsRequired = false },
            new { CategoryId = 16, AttributeId = 3, IsRequired = true },
            new { CategoryId = 15, AttributeId = 3, IsRequired = true },
            new { CategoryId = 15, AttributeId = 4, IsRequired = true },
            new { CategoryId = 23, AttributeId = 7, IsRequired = true },
            new { CategoryId = 9, AttributeId = 11, IsRequired = true },
            new { CategoryId = 9, AttributeId = 3, IsRequired = true },
            new { CategoryId = 26, AttributeId = 6, IsRequired = false },
            new { CategoryId = 28, AttributeId = 11, IsRequired = true },
            new { CategoryId = 6, AttributeId = 2, IsRequired = true },
            new { CategoryId = 3, AttributeId = 2, IsRequired = true },
            new { CategoryId = 3, AttributeId = 16, IsRequired = true },
            new { CategoryId = 4, AttributeId = 2, IsRequired = true },
            new { CategoryId = 4, AttributeId = 16, IsRequired = true },
            new { CategoryId = 7, AttributeId = 13, IsRequired = false },
            new { CategoryId = 28, AttributeId = 3, IsRequired = true },
            new { CategoryId = 29, AttributeId = 11, IsRequired = true },
            new { CategoryId = 30, AttributeId = 11, IsRequired = true });
    }
}