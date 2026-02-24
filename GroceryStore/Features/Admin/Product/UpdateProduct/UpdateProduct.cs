namespace GroceryStore.Features.Admin.Product.UpdateProduct;

using Shared.Interfaces;

public class UpdateProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Updates the product by ID")
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden);
    }

    public async static Task HandleAsync(
        UpdateProductRequest request,
        UpdateProductRepository repository,
        CancellationToken ct)
    {
    }
}