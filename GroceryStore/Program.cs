using GroceryStore.Bootstrap;
using Scalar.AspNetCore;

namespace GroceryStore;

using Database;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddBasicServices()
            .AddDatabaseServices(builder.Configuration)
            .AddFeatureServices();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.ApplyMigrations();
            app.MapScalarApiReference(o =>
                o.WithTheme(ScalarTheme.DeepSpace)
                    .WithTitle("Grocery Store")
                    .AddDocument("admin")
                    .AddDocument("user"));
        }

        app.UseHttpsRedirection();
        app.MapEndpointsGenerated();

        app.Run();
    }
}