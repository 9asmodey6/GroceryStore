namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using Abstractions;
using GroceryStore.Infrastructure;
using Mappers;
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
        CreateProductRequestValidator validator,
        ProductSkuGenerationService skuService,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var normalized = await normalizer.ValidateAndNormalizeAsync(request.CategoryId, request.Attributes, ct);
        var sku = await skuService.CreateSku(request.CategoryId, ct);
        var product = ProductMapper.ToEntity(
            request,
            sku,
            normalized);
        await repository.CreateAsync(product, ct);
        return Results.Created($"/api/v1/admin/products/{product.Id}", product);
    }
}