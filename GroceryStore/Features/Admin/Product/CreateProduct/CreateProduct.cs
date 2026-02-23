namespace GroceryStore.Features.Admin.Product.CreateProduct;

using Database.Entities.Product;
using Infrastructure.Services;
using Shared.Interfaces;

public class CreateProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/admin/products", HandleAsync)
            .WithTags("AdminProducts")
            .WithSummary("Creates a new Product")
            .WithName("CreateProduct")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden);
    }

    private static async Task<IResult> HandleAsync(
        CreateProductRequest request,
        CreateProductRepository repository,
        CategoryAttributeValueNormalizer normalizer,
        CreateProductRequestValidator validator,
        ProductSkuGenerationService skuService,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        Dictionary<int, string> normalized;

        try
        {
            normalized = await normalizer.ValidateAndNormalizeAsync(request.CategoryId, request.Attributes, ct);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }

        var sku = await skuService.CreateSku(request.CategoryId, ct);
        var product = ToEntity(request, sku, normalized);

        await repository.CreateAsync(product, ct);

        return Results.Created($"/api/v1/admin/products/{product.Id}", product);
    }

    private static Product ToEntity(
        CreateProductRequest request,
        string sku,
        Dictionary<int, string> normalized)
    {
        return new Product(
            request.Name,
            request.Price,
            request.CategoryId,
            sku,
            request?.Description,
            request?.BaseUnit,
            normalized);
    }
}