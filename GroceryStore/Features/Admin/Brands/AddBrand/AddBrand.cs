namespace GroceryStore.Features.Admin.Brands.AddBrand;

using System.Security.Claims;
using Database.Entities.Brand;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class AddBrand : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"/api/v1/admin/brands", HandleAsync)
            .WithTags("AdminBrands")
            .WithSummary("Add New Brand")
            .WithName("AddBrand");
    }

    public static async Task<Results<Created<Brand>, ValidationProblem, ForbidHttpResult>> HandleAsync(
        CancellationToken ct,
        AddBrandRequest request,
        AddBrandRepository repository,
        AddBrandValidator validator)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        var brand = ToEntity(request);

        await repository.AddBrandAsync(brand, ct);

        return TypedResults.Created($"/api/v1/admin/brands/{brand.Id}", brand);
    }

    public static Brand ToEntity(AddBrandRequest request)
    {
        return new Brand(
            request.Name);
    }
}