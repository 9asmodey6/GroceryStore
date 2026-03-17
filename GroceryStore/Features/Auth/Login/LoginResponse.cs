namespace GroceryStore.Features.Auth.Login;

public record LoginResponse(
    string Token,
    string Email,
    string FirstName);