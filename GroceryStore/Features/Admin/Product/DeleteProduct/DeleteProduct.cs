namespace GroceryStore.Features.Admin.Product.DeleteProduct;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class DeleteProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Performs soft removal of the product")
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden);
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