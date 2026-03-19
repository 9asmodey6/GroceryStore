namespace GroceryStore.Database.Configurations.Users;

using Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Consts;

public class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasData(new
        {
            Id = AdminSeedData.AdminUserId,
            UserName = "admin@store.com",
            NormalizedUserName = "ADMIN@STORE.COM",
            Email = "admin@store.com",
            NormalizedEmail = "ADMIN@STORE.COM",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "System",
            CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            SecurityStamp = "9ca06830-67d7-4632-9c32-f288924b893f",
            ConcurrencyStamp = "b7a5a87b-4024-46c5-8422-92182065842c",
            PasswordHash = "AQAAAAIAAYagAAAAEGMNZBXRiyiRbWqWDC6BZpBazgpNB5dAYizy/o2VwSgudvnw/5sqpGVlsFAp9P57WA==",
            AccessFailedCount = 0,
            LockoutEnabled = true,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
        });
    }
}