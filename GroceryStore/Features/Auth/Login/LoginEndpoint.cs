namespace GroceryStore.Features.Auth.Login;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Interfaces;

public class LoginEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/login",  HandleAsync)
            .WithTags("Login")
            .WithSummary("Login to Grocery Store")
            .WithGroupName(EndpointGroups.Auth);
    }

    private static async Task<Results<Ok<LoginResponse>, Microsoft.AspNetCore.Http.HttpResults.UnauthorizedHttpResult>> HandleAsync(
        LoginRequest request,
        LoginHandler handler,
        CancellationToken ct)
    {
        var user = await handler.GetUserAsync(request, ct);
        if (user == null)
        {
            return TypedResults.Unauthorized();
        }

        var isPasswordValid = await handler.CheckPasswordAsync(user, request, ct);
        if (!isPasswordValid)
        {
            return TypedResults.Unauthorized();
        }

        var token = await handler.GetTokenAsync(user, ct);
        return TypedResults.Ok(token);
    }
}