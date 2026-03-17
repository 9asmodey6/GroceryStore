namespace GroceryStore.Bootstrap;

using System.Data;
using System.Text;
using Dapper;
using Database;
using Database.Entities.User;
using Features.Admin.Brands.AddBrand;
using Features.Admin.Brands.DeleteBrand;
using Features.Admin.Brands.GetBrandById;
using Features.Admin.Brands.GetBrands;
using Features.Admin.Categories.GetCategories;
using Features.Admin.Countries.GetCountries;
using Features.Admin.Products.CreateProduct;
using Features.Admin.Products.DeleteProduct;
using Features.Admin.Products.GetProductById;
using Features.Admin.Products.GetProducts;
using Features.Admin.Products.UpdateProduct;
using FluentValidation;
using Infrastructure.Repositories.Categories;
using Infrastructure.Services;
using Mappers.Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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


    public static IServiceCollection AddAuthSevices(this IServiceCollection services, IConfiguration configuration)
    {
        // Identity
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // JWT
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "GroceryStore",
                    ValidAudience = "GroceryStore",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT_TOKEN_KEY"]
                                               ??
                                               throw new InvalidOperationException())),
                };
            });

        services.AddAuthorization();

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
        services.AddScoped<TokenService>();

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

        services.AddScoped<GetBrandByIdRepository>();

        return services;
    }

    [GenerateServiceRegistrations(AssignableTo = typeof(IEndpoint), CustomHandler = "MapEndpoint")]
    public static partial IEndpointRouteBuilder MapEndpointsGenerated(this IEndpointRouteBuilder endpoints);

    [GenerateServiceRegistrations(AssignableTo = typeof(IValidator<>), Lifetime = ServiceLifetime.Scoped)]
    public static partial IServiceCollection RegisterValidators(this IServiceCollection services);

    [GenerateServiceRegistrations(AssignableTo = typeof(IRepository), Lifetime = ServiceLifetime.Scoped)]
    public static partial IServiceCollection RegisterRepositories(this IServiceCollection services);
}