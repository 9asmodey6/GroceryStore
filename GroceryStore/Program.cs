using GroceryStore.Bootstrap;
using Scalar.AspNetCore;

namespace GroceryStore;

using Database;
using Shared.Consts;
using Shared.Consts.Endpoints;
using Shared.Extensions;
using Shared.Extensions.Logging;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ApplyConfigurations(builder.Configuration)
            .AddBasicServices()
            .AddSerilogLogging()
            .AddDatabaseServices(builder.Configuration)
            .AddFeatureServices()
            .AddAuthServices(builder.Configuration)
            .RegisterValidators()
            .RegisterRepositories();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.ApplyMigrations();

            app.MapScalarApiReference(o =>
                o.WithTheme(ScalarTheme.DeepSpace)
                    .WithTitle("Grocery Store")
                    .AddPreferredSecuritySchemes(OpenApiBearerScheme.Id)
                    .EnablePersistentAuthentication()
                    .AddDocument(EndpointGroups.Auth)
                    .AddDocument(EndpointGroups.Admin)
                    .AddDocument(EndpointGroups.User));
        }

        app.LogDocumentationLink();

        if (!app.Environment.IsProduction())
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseSerilogEnrichment();
        app.UseExceptionHandler(_ => { });
        app.UseAuthorization();

        app.MapEndpointsGenerated();

        app.Run();
    }
}