namespace GroceryStore.Database.Configurations.Attributes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

public class CategoryAttributeConfiguration : IEntityTypeConfiguration<CategoryAttribute>
{
    public void Configure(EntityTypeBuilder<CategoryAttribute> builder)
    {
        builder.ToTable("category_attributes");

        builder.HasKey(ca => new { ca.CategoryId, ca.AttributeId });
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.AttributeId).IsRequired();

        builder.HasOne(ca => ca.Category)
            .WithMany(c => c.CategoryAttributes)
            .HasForeignKey(ca => ca.CategoryId);

        builder.HasOne(ca => ca.Attribute)
            .WithMany()
            .HasForeignKey(ca => ca.AttributeId);
    }
}