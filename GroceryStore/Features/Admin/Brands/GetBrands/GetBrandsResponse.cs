namespace GroceryStore.Features.Admin.Brands.GetBrands;

public record GetBrandsResponse(
    GetBrandsResponseItem[] Entities);

public record GetBrandsResponseItem(
   int Id,
   string Name);