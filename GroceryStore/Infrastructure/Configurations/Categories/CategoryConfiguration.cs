namespace GroceryStore.Infrastructure.Configurations.Categories;

using GroceryStore.Domain.Entities.Category;
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

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(c => c.ParentId);

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        builder.HasMany(c => c.CategoryAttributes)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);
    }
}