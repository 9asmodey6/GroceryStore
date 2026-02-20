using Scalar.AspNetCore;
using Dapper;
using GroceryStore.Application;
using GroceryStore.Infrastructure.Connections;
using GroceryStore.Infrastructure.Dapper;
using GroceryStore.Infrastructure.Interfaces; 
using GroceryStore.Infrastructure.Persistence; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dapper connection factory
builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")!));

SqlMapper.AddTypeHandler(new JsonMetadataMapper());

builder.Services.AddFeaturesGenerated();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
        o.WithTheme(ScalarTheme.DeepSpace).WithTitle("Grocery Store"));
}

app.UseHttpsRedirection();

app.MapEndpointsGenerated();

app.Run();