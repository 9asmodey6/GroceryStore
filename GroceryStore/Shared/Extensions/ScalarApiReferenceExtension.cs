namespace GroceryStore.Shared.Extensions;

using Consts;
using Consts.Endpoints;
using Scalar.AspNetCore;

public static class ScalarApiReferenceExtension
{
    public static IApplicationBuilder ApplyScalarApiReference(this WebApplication app)
    {
        app.MapScalarApiReference(o =>
            o.WithTheme(ScalarTheme.DeepSpace)
                .WithTitle("Grocery Store")
                .AddPreferredSecuritySchemes(OpenApiBearerScheme.Id)
                .EnablePersistentAuthentication()
                .AddDocument(EndpointGroups.Auth)
                .AddDocument(EndpointGroups.Admin)
                .AddDocument(EndpointGroups.User));

        return app;
    }
}