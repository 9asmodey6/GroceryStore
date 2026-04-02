namespace GroceryStore.Features.Auth.Refresh;

using Shared.Consts.Endpoints;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;

public class RefreshTokenEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/refresh", async (
                    RefreshTokenHandler handler,
                    RefreshTokenRequest request,
                    HttpContext context)
                => TypedResults.Ok(await handler.HandleAsync(request, context)))
            .AllowAnonymous()
            .WithTags(EndpointTags.Auth)
            .WithSummary("Refresh Token")
            .WithGroupName(EndpointGroups.Auth);
    }
}