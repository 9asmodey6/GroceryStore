namespace GroceryStore.Features.Auth.Register;

using Database.Entities.User;
using Microsoft.AspNetCore.Identity;

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
            CreatedAt = DateTime.UtcNow,
        };

        return await userManager.CreateAsync(user, request.Password);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct)
    {
        return await userManager.FindByEmailAsync(email) == null;
    }
}