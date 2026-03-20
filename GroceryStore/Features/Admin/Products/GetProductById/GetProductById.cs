namespace GroceryStore.Features.Admin.Products.GetProductById;

using GroceryStore.Shared.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Extensions;

public class GetProductById : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/admin/products/{productId:int:min(1)}", HandleAsync)
            .WithTags(EndpointTags.AdminProducts)
            .RequireAdminRole()
            .WithSummary("Get Product by Id")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Results<Ok<GetProductByIdResponse>, NotFound>> HandleAsync(
        GetProductByIdRepository repository,
        int productId,
        CancellationToken ct)
    {
        var response = await repository.GetProductByIdAsync(productId, ct);

        if (response == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(response);
    }
}