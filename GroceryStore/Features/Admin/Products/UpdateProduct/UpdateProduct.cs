namespace GroceryStore.Features.Admin.Products.UpdateProduct;

using FluentValidation;
using GroceryStore.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Extensions;

public class UpdateProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithValidation<UpdateProductRequest>()
            .WithTags("AdminProducts")
            .WithSummary("Updates the product by ID")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Results<Ok<UpdateProductResponse>, NotFound, ValidationProblem>> HandleAsync(
        int productId,
        UpdateProductRequest request,
        UpdateProductRepository repository,
        UpdateProductHandler handler,
        CancellationToken ct)
    {
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