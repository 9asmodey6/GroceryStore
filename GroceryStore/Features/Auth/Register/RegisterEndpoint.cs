namespace GroceryStore.Features.Auth.Register;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Extensions;
using Shared.Interfaces;

public class RegisterEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/register",  HandleAsync)
            .AllowAnonymous()
            .WithValidation<RegisterRequest>()
            .WithTags(EndpointTags.Auth)
            .WithSummary("Register New User")
            .WithGroupName(EndpointGroups.Auth);
    }

    private static async Task<Results<Ok<RegisterResponse>, BadRequest<IEnumerable<IdentityError>>, ValidationProblem>> HandleAsync(
        RegisterRequest request,
        RegisterHandler handler)
    {
        var result = await handler.RegisterAsync(request);

        if (!result.Succeeded)
        {
            return TypedResults.BadRequest(result.Errors);
        }

        return TypedResults.Ok(new RegisterResponse(request.Email, request.FirstName));
    }
}