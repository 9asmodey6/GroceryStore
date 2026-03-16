namespace GroceryStore.Features.Admin.Brands.GetBrands;

public record GetBrandsResponse(
    GetBrandsResponseItem[] Items);

public record GetBrandsResponseItem(
   int Id,
   string Name);