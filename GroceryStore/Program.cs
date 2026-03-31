using GroceryStore.Bootstrap;

namespace GroceryStore;

using Database;
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

            app.ApplyScalarApiReference();
        }

        app.LogDocumentationLink();

        app.UseRouting();
        app.MapHealthChecks("/health");
        app.UseAuthentication();
        app.UseSerilogEnrichment();
        app.UseExceptionHandler(_ => { });
        app.UseAuthorization();

        app.MapEndpointsGenerated();

        app.Run();
    }
}