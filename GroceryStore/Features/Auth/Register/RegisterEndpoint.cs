namespace GroceryStore.Features.Auth.Register;

using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Consts;
using Shared.Extensions;
using Shared.Interfaces;

public class RegisterEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/register",  HandleAsync)
            .WithValidation<RegisterRequest>()
            .WithTags("Auth")
            .WithSummary("Register New User")
            .WithGroupName(EndpointGroups.Auth);
    }

    private static async Task<Results<BadRequest, ValidationProblem>> HandleAsync(RegisterRequest request)
    {
        return TypedResults.BadRequest();
    }
}