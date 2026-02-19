using Scalar.AspNetCore;
using Dapper;
using GroceryStore.Infrastructure.Connections;
using GroceryStore.Infrastructure.Dapper;
using GroceryStore.Infrastructure.Interfaces;
using GroceryStore.Infrastructure.Persistence;
using GroceryStore.Application.Services;
using GroceryStore.Application.Features.Admin.Categories.GetMetadataByCategoryId;
using GroceryStore.Application.Features.Admin.Product.CreateProduct;
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

// Application services
builder.Services.AddScoped<CategoryAttributeService>();
builder.Services.AddScoped<CategoryAttributeValueNormalizer>();
builder.Services.AddScoped<CreateProductRequestValidator>();
builder.Services.AddScoped<ProductSkuGenerationService>();
builder.Services.AddScoped<CreateProductRepository>();

// Repositories (Dapper)
builder.Services.AddScoped<GetMetadataRepository>();

// (пока пустой, но чтобы DI не ругался)
builder.Services.AddScoped<CreateProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
        o.WithTheme(ScalarTheme.DeepSpace).WithTitle("Grocery Store"));
}

app.UseHttpsRedirection();

// Временно — ручной маппинг эндпоинтов (сразу увидишь их в Scalar)
new GetMetadataEndpoint().MapEndpoint(app);
new CreateProduct().MapEndpoint(app);

app.Run();