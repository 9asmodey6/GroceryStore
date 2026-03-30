namespace GroceryStore.Infrastructure.Handlers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Shared.Exceptions;

public class ProblemDetailsAuthorizationHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult result)
    {
        if (result.Challenged)
        {
            throw new UnauthorizedAccessException("Authentication is required to access this resource.");
        }

        if (result.Forbidden)
        {
            throw new ForbiddenAccessException("You do not have the necessary permissions.");
        }

        await _defaultHandler.HandleAsync(next, context, policy, result);
    }
}