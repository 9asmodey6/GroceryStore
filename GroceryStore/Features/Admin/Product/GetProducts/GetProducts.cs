namespace GroceryStore.Features.Admin.Product.GetProducts;

using Shared.Interfaces;

public class GetProducts : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/products", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Get all active Products with attributes")
            .WithName("GetProducts")
            .Produces<IEnumerable<GetProductsResponse>>();
    }

    private static async Task<IResult> HandleAsync(
        GetProductsRepository repository,
        CancellationToken ct)
    {
        var result = await repository.GetProductsAsync(ct);
        return Results.Ok(result);
    }
}