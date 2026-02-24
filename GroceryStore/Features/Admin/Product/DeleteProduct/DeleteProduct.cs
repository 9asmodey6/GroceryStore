namespace GroceryStore.Features.Admin.Product.DeleteProduct;

using CreateProduct;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class DeleteProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/admin/products/{productId}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Performs soft removal of the product")
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<Results<NoContent, NotFound>> HandleAsync(
        int productId,
        DeleteProductRepository repository,
        CancellationToken ct)
    {
        var deleted = await repository.DeleteProductByIdAsync(productId, ct);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }
}