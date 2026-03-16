namespace GroceryStore.Features.Admin.Brands.AddBrand;

using Database.Entities.Brand;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Shared.Consts;
using Shared.Interfaces;

public class AddBrand : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/admin/brands", HandleAsync)
            .WithTags("AdminBrands")
            .WithSummary("Add new Brand")
            .WithGroupName("admin");
    }

    private static async Task<Results<Created<AddBrandResponse>, ValidationProblem>> HandleAsync(
        AddBrandRequest request,
        AddBrandRepository repository,
        IMemoryCache cache,
        IValidator<AddBrandRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        var brand = ToEntity(request);

        await repository.AddBrandAsync(brand, ct);

        cache.Remove(LookupCacheKeys.Brands);

        return TypedResults.Created($"/api/v1/admin/brands/{brand.Id}",  new AddBrandResponse(brand.Name));
    }

    private static Brand ToEntity(AddBrandRequest request)
    {
        return new Brand(
            request.Name);
    }
}