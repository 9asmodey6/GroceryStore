using GroceryStore.Bootstrap;

namespace GroceryStore;

using Database;
using Serilog;
using Shared.Consts;
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

        builder.Services.AddAppHealthChecks(builder.Configuration);

        var app = builder.Build();

        app.UseExceptionHandler(_ => { });

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
        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = SerilogConsts.MessageTemplate;
        });
        app.UseAuthorization();

        app.MapEndpointsGenerated();

        app.Run();
    }
}