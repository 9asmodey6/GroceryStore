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
            },
            new IdentityRole
            {
                Id = AdminSeedData.UserRoleId,
                Name = UserRoles.User,
                NormalizedName = UserRoles.User.ToUpper(),
            });
    }
}