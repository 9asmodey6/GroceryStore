using Scalar.AspNetCore;
using GroceryStore.Infrastructure;
using Dapper;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection") !));
SqlMapper.AddTypeHandler(new JsonTypeHandler());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTheme(ScalarTheme.DeepSpace)
            .WithTitle("Grocery Store");
    });
}

app.UseHttpsRedirection();

app.Run();