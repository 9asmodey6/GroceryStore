namespace GroceryStore.Bootstrap;

using System.Data;
using Dapper;
using Database;
using Features.Admin.Product.CreateProduct;
using Infrastructure.Dapper;
using Infrastructure.Repositories.Categories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using ServiceScan.SourceGenerator;
using Shared.Interfaces;

public static partial class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBasicServices()
        {
            services.AddOpenApi();
            services.AddMemoryCache();

            return services;
        }

        public IServiceCollection AddDatabaseServices(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new DbConnectionFactory(connectionString));
            
            SqlMapper.AddTypeHandler(new JsonMetadataMapper());
            
            return services;
        }
        
        public IServiceCollection AddFeatureServices()
        {
            services.AddScoped<CreateProductRepository>();
            services.AddScoped<CategoryAttributeValueNormalizer>();
            services.AddScoped<ProductSkuGenerationService>();
            services.AddScoped<CreateProductRequestValidator>();

            services.AddScoped<CategoryAttributeRepository>();

            return services;
        }
    }
    [GenerateServiceRegistrations(AssignableTo = typeof(IEndpoint), CustomHandler = "MapEndpoint")]
    public static partial IEndpointRouteBuilder MapEndpointsGenerated(this IEndpointRouteBuilder endpoints);
}
