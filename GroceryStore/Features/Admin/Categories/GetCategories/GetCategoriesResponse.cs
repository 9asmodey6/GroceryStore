namespace GroceryStore.Features.Admin.Categories.GetCategories;

public record GetCategoriesResponse(
    GetCategoriesResponseItem[] Entities);

public record GetCategoriesResponseItem(
    int Id,
    string Name,
    int? ParentId,
    string? FullPath,
    int Level);