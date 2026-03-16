namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using GroceryStore.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

public class UpdateProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Updates the product by ID")
            .WithGroupName("admin");
    }

    private static async Task<Results<Ok<UpdateProductResponse>, NotFound, ValidationProblem>> HandleAsync(
        int productId,
        UpdateProductRequest request,
        UpdateProductRepository repository,
        FluentValidation.IValidator<UpdateProductRequest> validator,
        UpdateProductHandler handler,
        CancellationToken ct)
    {
        var validatedRequest = await validator.ValidateAsync(request, ct);
        if (!validatedRequest.IsValid)
        {
            return TypedResults.ValidationProblem(validatedRequest.ToDictionary());
        }

        var productToUpdate = await repository.GetById(productId, ct);
        if (productToUpdate is null)
        {
            return TypedResults.NotFound();
        }

        handler.Apply(productToUpdate, request);

        await repository.SaveChangesAsync(ct);

        var response = new UpdateProductResponse(
            productToUpdate.Id,
            productToUpdate.Name,
            productToUpdate.Price,
            productToUpdate.SKU,
            productToUpdate.Description,
            productToUpdate.BaseUnit);

        return TypedResults.Ok(response);
    }
}