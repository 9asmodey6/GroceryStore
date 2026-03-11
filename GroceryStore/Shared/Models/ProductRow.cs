namespace GroceryStore.Shared.Models;

public sealed record ProductRow(
    int Id,
    string Name,
    decimal Price,
    string CategoryName,
    string BrandName,
    string CountryCode,
    string Sku,
    string? Description,
    string BaseUnit,
    string? AttributeName,
    string? AttributeUnit,
    string? AttributeValue);