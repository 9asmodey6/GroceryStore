namespace GroceryStore.Bootstrap;

using System.Data;
using Dapper;
using Database;
using Features.Admin.Brands.AddBrand;
using Features.Admin.Brands.DeleteBrand;
using Features.Admin.Brands.GetBrands;
using Features.Admin.Categories.GetCategories;
using Features.Admin.Countries.GetCountries;
using Features.Admin.Products.CreateProduct;
using Features.Admin.Products.DeleteProduct;
using Features.Admin.Products.GetProductById;
using Features.Admin.Products.GetProducts;
using Features.Admin.Products.UpdateProduct;
using Infrastructure.Repositories.Categories;
using Infrastructure.Services;
using Mappers.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ServiceScan.SourceGenerator;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.Optional;

public static partial class DependencyInjection
{
    public static IServiceCollection AddBasicServices(this IServiceCollection services)
    {
        services.AddOpenApi("admin", options =>
        {
            options.ShouldInclude = desc => desc.GroupName == "admin";

            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "GroceryStore Admin API",
                    Version = "v1",
                };

                return Task.CompletedTask;
            });
        });

        services.AddOpenApi("user", options =>
        {
            options.ShouldInclude = desc => desc.GroupName == "user";

            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "GroceryStore User API",
                    Version = "v1",
                };

                return Task.CompletedTask;
            });
        });

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

        services.AddScoped<CategoryAttributeRepository>();

        services.AddScoped<GetProductsRepository>();

        services.AddScoped<DeleteProductRepository>();

        services.AddScoped<UpdateProductRepository>();
        services.AddScoped<UpdateProductHandler>();

        services.AddScoped<GetCategoriesRepository>();

        services.AddScoped<GetProductByIdRepository>();

        services.AddScoped<GetBrandsRepository>();

        services.AddScoped<AddBrandRepository>();

        services.AddScoped<GetCountriesRepository>();

        services.AddScoped<DeleteBrandRepository>();

        return services;
    }

    [GenerateServiceRegistrations(AssignableTo = typeof(IEndpoint), CustomHandler = "MapEndpoint")]
    public static partial IEndpointRouteBuilder MapEndpointsGenerated(this IEndpointRouteBuilder endpoints);

    [GenerateServiceRegistrations(AssignableTo = typeof(FluentValidation.AbstractValidator<>), Lifetime = ServiceLifetime.Scoped)]
    public static partial IServiceCollection RegisterValidators(this IServiceCollection services);
}