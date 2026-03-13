namespace GroceryStore.Features.Admin.Categories.GetCategories;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class GetCategories : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/categories", HandleAsync)
            .WithTags("AdminCategories")
            .WithSummary("Get all categories")
            .WithGroupName("admin");
    }

    private static async Task<Ok<GetCategoriesResponse>> HandleAsync(
        GetCategoriesRepository repository,
        CancellationToken ct)
    {
        var result = await repository.GetCategoriesAsync(ct);
        return TypedResults.Ok(result);
    }
}