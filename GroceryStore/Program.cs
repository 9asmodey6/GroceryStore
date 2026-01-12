using Scalar.AspNetCore;
using GroceryStore.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection") !));

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