namespace GroceryStore.Features.Auth.Login;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts.Endpoints;
using Shared.Interfaces;
using Shared.Interfaces.Repositories;

public class LoginEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/login", HandleAsync)
            .AllowAnonymous()
            .WithTags(EndpointTags.Auth)
            .WithSummary("Sign In")
            .WithGroupName(EndpointGroups.Auth);
    }

    private static async Task<Results<Ok<LoginResponse>, UnauthorizedHttpResult>> HandleAsync(
        LoginRequest request,
        LoginHandler handler,
        ILogger<LoginEndpoint> logger,
        CancellationToken ct)
    {
        logger.LogInformation("Login attempt for {Email}", request.Email);

        var user = await handler.GetUserAsync(request);

        if (user == null)
        {
            logger.LogWarning("Login failed: user with email {Email} not found.", request.Email);
            return TypedResults.Unauthorized();
        }

        var isPasswordValid = await handler.CheckPasswordAsync(user, request);
        if (!isPasswordValid)
        {
            logger.LogWarning("Login failed: invalid password for user {Email}.", request.Email);
            return TypedResults.Unauthorized();
        }

        var loginResponse = await handler.CreateLoginResponseAsync(user);
        logger.LogInformation("User {Email} logged in successfully. Tokens generated.", request.Email);
        return TypedResults.Ok(loginResponse);
    }
}