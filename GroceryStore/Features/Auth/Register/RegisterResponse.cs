namespace GroceryStore.Features.Auth.Register;

public record RegisterResponse(
    int Id,
    string Email,
    string FirstName);