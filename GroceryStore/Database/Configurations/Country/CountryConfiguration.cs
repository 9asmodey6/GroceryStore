namespace GroceryStore.Database.Configurations.Country;

using Entities.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(2);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

        builder.HasIndex(x => x.Code).IsUnique();

        builder.HasData(
            new { Id = 1, Code = "UA", Name = "Ukraine" },
            new { Id = 2, Code = "PL", Name = "Poland" },
            new { Id = 3, Code = "DE", Name = "Germany" }
        );
    }
}