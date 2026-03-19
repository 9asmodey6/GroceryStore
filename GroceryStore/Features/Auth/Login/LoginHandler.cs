namespace GroceryStore.Features.Auth.Login;

using Database.Entities.User;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

public class LoginHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService service)
{
    public async Task<AppUser?> GetUserAsync(LoginRequest request)
    {
        return await userManager.FindByEmailAsync(request.Email);
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, LoginRequest request)
    {
        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        return result.Succeeded;
    }

    public async Task<LoginResponse> GetTokenAsync(AppUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var token = service.GenerateAccessToken(user, roles);

        return new LoginResponse(
            Token: token,
            Email: user.Email!,
            FirstName: user.FirstName,
            LastName: user.LastName);
    }
}