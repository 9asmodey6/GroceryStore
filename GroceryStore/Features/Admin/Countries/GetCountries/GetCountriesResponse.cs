namespace GroceryStore.Features.Admin.Countries.GetCountries;

public record GetCountriesResponse(
   GetCountriesResponseItem[] Items);

public record GetCountriesResponseItem(
   int Id,
   string Name,
   string Code);
