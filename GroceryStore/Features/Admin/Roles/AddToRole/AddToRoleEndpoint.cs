namespace GroceryStore.Features.Admin.Roles.AddToRole;

using System.Security.Claims;
using Database.Entities.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Extensions;
using Shared.Interfaces;

public class AddToRoleEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/admin/roles", HandleAsync)
            .RequireAdminRole()
            .WithTags(EndpointTags.AdminRoles)
            .WithSummary("Add User to Admin role")
            .WithGroupName(EndpointGroups.Admin);
    }

    private static async Task<Results<NotFound<string>, BadRequest<string>, Ok<string>>> HandleAsync(
        AddToRoleRequest request,
        UserManager<AppUser> userManager,
        HttpContext context,
        ILogger<AddToRoleEndpoint> logger)
    {
        var adminEmail = context.User.FindFirstValue(ClaimTypes.Email);

        logger.LogInformation(
            "Admin {AdminEmail} is trying to add user {TargetEmail} to admin role",
            adminEmail,
            request.Email);

        var userToAddRole = await userManager.FindByEmailAsync(request.Email);

        if (userToAddRole == null)
        {
            logger.LogWarning("Failed to add user {Email} to role: User not found", request.Email);
            return TypedResults.NotFound("User not found");
        }

        var userRoles = await userManager.GetRolesAsync(userToAddRole);

        if (userRoles.Contains(UserRoles.Admin))
        {
            logger.LogWarning("Failed to add user {Email} to role: User already has Admin role", request.Email);
            return TypedResults.BadRequest("User is already in Admin role");
        }

        var result = await userManager.AddToRoleAsync(userToAddRole, UserRoles.Admin);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("Failed to add user {Email} to role. Errors: {Errors}", request.Email, errors);
            throw new Exception("Internal error while adding user to role");
        }

        logger.LogInformation(
            "User {Email} successfully added to Admin role by {AdminEmail}",
            request.Email,
            adminEmail);
        return TypedResults.Ok("User successfully added to Admin role");
    }
}

