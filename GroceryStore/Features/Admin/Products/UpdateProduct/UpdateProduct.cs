namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using GroceryStore.Database.Entities.Product;
using GroceryStore.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

public class UpdateProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Updates the product by ID")
            .WithName("UpdateProduct");
    }

    private static async Task<Results<Created<Product>, NotFound, ValidationProblem, ForbidHttpResult>> HandleAsync(
        int productId,
        UpdateProductRequest request,
        UpdateProductRepository repository,
        UpdateProductValidator validator,
        UpdateProductHandler handler,
        CancellationToken ct)
    {
        var productToUpdate = await repository.GetById(productId, ct);
        if (productToUpdate is null)
        {
            return TypedResults.NotFound();
        }

        var validatedRequest = await validator.ValidateAsync(request, ct);
        if (!validatedRequest.IsValid)
        {
            return TypedResults.ValidationProblem(validatedRequest.ToDictionary());
        }

        handler.Apply(productToUpdate, request);

        await repository.SaveChangesAsync(ct);

        return TypedResults.Created($"/api/v1/admin/products/{productToUpdate.Id}", productToUpdate);
    }
}