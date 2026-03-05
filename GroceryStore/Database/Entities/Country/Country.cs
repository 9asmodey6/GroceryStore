namespace GroceryStore.Database.Entities.Country;

using Shared;

public class Country : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}