namespace GroceryStore.Features.Admin.Categories.GetCategories;

public record GetCategoriesResponse(
    int Id,
    string Name,
    int? ParentId,
    string? FullPath,
    int Level);