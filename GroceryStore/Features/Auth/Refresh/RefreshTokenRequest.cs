namespace GroceryStore.Features.Auth.Refresh;

public record RefreshTokenRequest(
    string AccessToken,
    string RefreshToken);