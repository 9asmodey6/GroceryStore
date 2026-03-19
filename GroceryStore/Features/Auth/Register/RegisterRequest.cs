namespace GroceryStore.Features.Auth.Register;

public record RegisterRequest(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Password);