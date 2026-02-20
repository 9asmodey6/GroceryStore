namespace GroceryStore.Infrastructure.Configurations.CategoryAttributes;

using GroceryStore.Domain.Entities.CategoryAttribute;
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
    }
}