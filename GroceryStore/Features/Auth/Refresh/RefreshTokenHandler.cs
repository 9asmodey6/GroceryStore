namespace GroceryStore.Features.Auth.Refresh;

using System.Security.Claims;
using Database.Entities.User;
using Infrastructure.Services;
using Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class RefreshTokenHandler(
    UserManager<AppUser> userManager,
    TokenService service,
    ILogger<RefreshTokenHandler> logger)
{
    public async Task<RefreshTokenResponse> HandleAsync(RefreshTokenRequest request, HttpContext httpContext)
    {
        var principal = service.GetPrincipalFromExpiredToken(request.AccessToken);

        httpContext.User = principal;

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            var availableClaims = string.Join(", ", principal.Claims.Select(c => c.Type));

            logger.LogWarning(
                "Token validation succeeded, but critical claim '{ClaimType}' is missing. " +
                "Available claims in token: [{Claims}]. " +
                "Check TokenService.GenerateAccessToken logic.",
                ClaimTypes.NameIdentifier,
                availableClaims);

            throw new SecurityTokenException("Invalid token claims");
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            logger.LogWarning("Refresh failed: User {UserId} not found in database.", userId);
            throw new SecurityTokenException("User no longer exists");
        }

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            logger.LogWarning("Refresh failed: Invalid or expired refresh token for user {Email}.", user.Email);
            throw new SecurityTokenException("Invalid refresh token");
        }

        var roles = await userManager.GetRolesAsync(user);
        var newAccessToken = service.GenerateAccessToken(user, roles);
        var newRefreshTokenDto = service.GenerateRefreshToken();

        user.RefreshToken = newRefreshTokenDto.Token;
        user.RefreshTokenExpiryTime = newRefreshTokenDto.ExpiryTime;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("Refresh failed: Could not update user {Email} in DB. Errors: {Errors}", user.Email,
                errors);
            throw new Exception("Error updating user session");
        }

        logger.LogInformation("Tokens successfully rotated for user {Email}.", user.Email);

        return new RefreshTokenResponse(
            Token: newAccessToken,
            RefreshToken: user.RefreshToken,
            Email: user.Email!,
            FirstName: user.FirstName,
            LastName: user.LastName);
    }
}