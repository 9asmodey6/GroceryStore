namespace GroceryStore.Features.Auth.Login;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Interfaces;

public class LoginEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/login",  HandleAsync)
            .AllowAnonymous()
            .WithTags(EndpointTags.Auth)
            .WithSummary("Sign In")
            .WithGroupName(EndpointGroups.Auth);
    }

    private static async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult>> HandleAsync(
        LoginRequest request,
        LoginHandler handler,
        CancellationToken ct)
    {
        var user = await handler.GetUserAsync(request);
        if (user == null)
        {
            return TypedResults.Unauthorized();
        }

        var isPasswordValid = await handler.CheckPasswordAsync(user, request);
        if (!isPasswordValid)
        {
            return TypedResults.Unauthorized();
        }

        var token = await handler.GetTokenAsync(user);
        return TypedResults.Ok(token);
    }
}