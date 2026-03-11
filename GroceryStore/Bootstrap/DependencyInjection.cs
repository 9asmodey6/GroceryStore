namespace GroceryStore.Bootstrap;

using System.Data;
using Dapper;
using Database;
using Features.Admin.Categories.GetCategories;
using Features.Admin.Product.CreateProduct;
using Features.Admin.Product.DeleteProduct;
using Features.Admin.Product.GetProductById;
using Features.Admin.Product.GetProducts;
using Features.Admin.Product.UpdateProduct;
using Infrastructure.Dapper;
using Infrastructure.Repositories.Categories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using ServiceScan.SourceGenerator;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.Optional;

public static partial class DependencyInjection
{
    public static IServiceCollection AddBasicServices(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddMemoryCache();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new OptionalJsonConverterFactory());
        });

        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new JsonMetadataMapper());

        return services;
    }

    public static IServiceCollection AddFeatureServices(this IServiceCollection services)
    {
        services.AddScoped<CreateProductRepository>();
        services.AddScoped<CategoryAttributeValueNormalizer>();
        services.AddScoped<ProductSkuGenerationService>();
        services.AddScoped<CreateProductRequestValidator>();

        services.AddScoped<CategoryAttributeRepository>();

        services.AddScoped<GetProductsRepository>();

        services.AddScoped<DeleteProductRepository>();

        services.AddScoped<UpdateProductRepository>();
        services.AddScoped<UpdateProductHandler>();
        services.AddScoped<UpdateProductValidator>();

        services.AddScoped<GetCategoriesRepository>();

        services.AddScoped<GetProductByIdRepository>();

        return services;
    }

    [GenerateServiceRegistrations(AssignableTo = typeof(IEndpoint), CustomHandler = "MapEndpoint")]
    public static partial IEndpointRouteBuilder MapEndpointsGenerated(this IEndpointRouteBuilder endpoints);
}