namespace GroceryStore.Application.Features.Admin.Product.CreateProduct;

using Abstractions;
using Services;

public class CreateProductFeature : IFeature
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<CreateProductRepository>();
        services.AddScoped<CategoryAttributeValueNormalizer>();
        services.AddScoped<ProductSkuGenerationService>();
        services.AddScoped<CreateProductRequestValidator>();
    }
}