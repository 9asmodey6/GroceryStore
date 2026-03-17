namespace GroceryStore.Features.Auth.Login;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Interfaces;

public class LoginEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        throw new NotImplementedException();
    }

    private static async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult>> HandleAsync(
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