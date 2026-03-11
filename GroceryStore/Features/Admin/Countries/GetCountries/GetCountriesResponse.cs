namespace GroceryStore.Features.Admin.Countries.GetCountries;

public record GetCountriesResponse(
    int Id,
    string Name,
    string Code);
