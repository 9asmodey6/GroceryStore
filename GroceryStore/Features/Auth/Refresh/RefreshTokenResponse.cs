namespace GroceryStore.Features.Auth.Refresh;

public record RefreshTokenResponse(
    string Token,
    string RefreshToken,
    string Email,
    string FirstName,
    string LastName);