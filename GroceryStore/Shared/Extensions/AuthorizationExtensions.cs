namespace GroceryStore.Shared.Extensions;

using Consts;

public static class AuthorizationExtensions
{
    public static RouteHandlerBuilder RequireAdminRole(this RouteHandlerBuilder builder)
    {
        return builder.RequireAuthorization(policy => 
            policy.RequireRole(UserRoles.Admin));
    }
}