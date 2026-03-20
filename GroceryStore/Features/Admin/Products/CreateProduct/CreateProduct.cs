namespace GroceryStore.Features.Admin.Products.CreateProduct;

using Database.Entities.Product;
using FluentValidation;
using Infrastructure.Services;
using Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Extensions;

public class CreateProduct : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/admin/products", HandleAsync)
            .WithValidation<CreateProductRequest>()
            .RequireAdminRole()
            .WithTags(EndpointTags.AdminProducts)
            .WithSummary("Creates a new Product")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Results<Created<CreateProductResponse>, ValidationProblem, BadRequest<string>>> HandleAsync(
        CreateProductRequest request,
        CreateProductRepository repository,
        CategoryAttributeValueNormalizer normalizer,
        ProductSkuGenerationService skuService,
        CancellationToken ct)
    {
        Dictionary<int, string> normalized;

        try
        {
            var norm = await normalizer.ValidateAndNormalizeAsync(request.CategoryId, request.Attributes, ct);

            if (!norm.IsSuccess)
            {
                var errors = norm.Validation.Errors
                    .GroupBy(e => e.Field ?? "attributes")
                    .ToDictionary(g => g.Key, g => g
                        .Select(x => x.Message)
                        .ToArray());

                return TypedResults.ValidationProblem(errors);
            }

            normalized = norm.Value!;
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }

        var sku = await skuService.CreateSku(request.CategoryId, ct);
        var product = ToEntity(request, sku, normalized);

        await repository.CreateAsync(product, ct);
        var response = new CreateProductResponse(
            product.Id,
            product.Name,
            product.Price,
            product.SKU,
            product.Description,
            product.BaseUnit);

        return TypedResults.Created($"/api/v1/admin/products/{product.Id}", response);
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
            request.BrandId,
            request.CountryId,
            sku,
            request?.Description,
            request?.BaseUnit,
            normalized);
    }
}