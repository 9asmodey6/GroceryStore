using GroceryStore.Bootstrap;
using Scalar.AspNetCore;

namespace GroceryStore;

using Database;
using Database.Entities.User;
using Microsoft.AspNetCore.Identity;
using Shared.Consts;
using Shared.Extensions;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddBasicServices()
            .AddDatabaseServices(builder.Configuration)
            .AddFeatureServices()
            .AddAuthSevices(builder.Configuration)
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
                    .AddDocument(EndpointGroups.Auth)
                    .AddDocument(EndpointGroups.Admin)
                    .AddDocument(EndpointGroups.User));
        }

        app.LogDocumentationLink();

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapEndpointsGenerated();

        app.Run();
    }
}