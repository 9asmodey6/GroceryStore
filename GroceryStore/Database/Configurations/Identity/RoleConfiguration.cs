namespace GroceryStore.Database.Configurations.Identity;

using Shared.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = AdminSeedData.AdminRoleId,
                Name = UserRoles.Admin,
                NormalizedName = UserRoles.Admin.ToUpper(),
                ConcurrencyStamp = "b7a5a87b-4024-46c5-8422-92182065842c",
            },
            new IdentityRole
            {
                Id = AdminSeedData.UserRoleId,
                Name = UserRoles.User,
                NormalizedName = UserRoles.User.ToUpper(),
                ConcurrencyStamp = "c8b6b98c-5135-47d6-9533-03293176953d",
            });
    }
}