namespace GroceryStore.Features.Admin.Brands.GetBrandById;

using Database;

public class GetBrandByIdRepository(AppDbContext dbContext)
{
    public async Task<GetBrandByIdResponse?> GetAsync(int id, CancellationToken ct)
    {
        var brand = await dbContext.Brands.FindAsync(id, ct);

        if (brand == null)
        {
            return null;
        }

        return new GetBrandByIdResponse(brand.Id, brand.Name);
    }
}