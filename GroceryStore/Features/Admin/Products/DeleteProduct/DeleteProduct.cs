namespace GroceryStore.Features.Admin.Products.DeleteProduct;

using GroceryStore.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

public class DeleteProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Performs soft removal of the product")
            .WithGroupName("admin");
    }

    private static async Task<Results<NoContent, NotFound>> HandleAsync(
        int productId,
        DeleteProductRepository repository,
        CancellationToken ct)
    {
        var isDeleted = await repository.DeleteProductByIdAsync(productId, ct);

        return isDeleted
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}