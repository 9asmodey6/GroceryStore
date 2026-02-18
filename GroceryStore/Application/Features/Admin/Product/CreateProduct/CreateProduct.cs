namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using Abstractions;
using GroceryStore.Infrastructure;
using Services;

public class CreateProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/admin/products", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Creates a new Product")
            .WithName("CreateProduct")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden);
    }

    static async Task<IResult> HandleAsync(
        CreateProductRequest request,
        CreateProductRepository repository,
        CategoryAttributeValueNormalizer normalizer,
        CancellationToken ct)
    {
        var normalized = normalizer.ValidateAndNormalizeAsync(request.CategoryId,request.Attributes, ct);
        return Results.Ok(normalized);
    }
}