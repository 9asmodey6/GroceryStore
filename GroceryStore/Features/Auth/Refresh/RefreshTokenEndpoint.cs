namespace GroceryStore.Features.Auth.Refresh;

using Shared.Consts.Endpoints;
using Shared.Interfaces;

public class RefreshTokenEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/refresh", async (RefreshTokenHandler handler, RefreshTokenRequest request)
                => TypedResults.Ok(await handler.HandleAsync(request)))
            .AllowAnonymous()
            .WithTags(EndpointTags.Auth)
            .WithSummary("Refresh Token")
            .WithGroupName(EndpointGroups.Auth);
    }
}