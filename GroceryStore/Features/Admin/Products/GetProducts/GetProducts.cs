namespace GroceryStore.Features.Admin.Products.GetProducts;

using GroceryStore.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

public class GetProducts : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/products", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Get all active Products with attributes")
            .WithName("GetProducts");
    }

    private static async Task<Results<ForbidHttpResult, Ok<List<GetProductsResponse>>>> HandleAsync(
        GetProductsRepository repository,
        CancellationToken ct)
    {
        var result = await repository.GetProductsAsync(ct);
        return TypedResults.Ok(result.ToList());
    }
}