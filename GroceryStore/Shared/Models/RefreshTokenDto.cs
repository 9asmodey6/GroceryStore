namespace GroceryStore.Shared.Models;

public record RefreshTokenDto(string Token, DateTime ExpiryTime);