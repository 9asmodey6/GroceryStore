namespace GroceryStore.Features.Auth.Login;

using Database.Entities.User;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

public class LoginHandler(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    TokenService service,
    ILogger<LoginHandler> logger)
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

    public async Task<LoginResponse> CreateLoginResponseAsync(AppUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var accessToken = service.GenerateAccessToken(user, roles);

        var refreshTokenDto = service.GenerateRefreshToken();

        user.RefreshToken = refreshTokenDto.Token;
        user.RefreshTokenExpiryTime = refreshTokenDto.ExpiryTime;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors
                .Select(e => e.Description));
            logger.LogError(
                "Failed to update user {Email} with refresh token. Errors: {Errors}",
                user.Email,
                errors);

            throw new Exception("Internal error during login session creation");
        }

        return new LoginResponse(
            Token: accessToken,
            RefreshToken: user.RefreshToken,
            Email: user.Email!,
            FirstName: user.FirstName,
            LastName: user.LastName);
    }
}