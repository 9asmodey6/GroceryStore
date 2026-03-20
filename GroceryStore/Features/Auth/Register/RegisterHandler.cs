namespace GroceryStore.Features.Auth.Register;

using Database.Entities.User;
using Microsoft.AspNetCore.Identity;
using Shared.Consts;

public class RegisterHandler(UserManager<AppUser> userManager)
{
    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return result;
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct)
    {
        return await userManager.FindByEmailAsync(email) == null;
    }
}