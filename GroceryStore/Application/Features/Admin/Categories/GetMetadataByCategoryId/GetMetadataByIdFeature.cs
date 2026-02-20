namespace GroceryStore.Application.Features.Admin.Categories.GetMetadataByCategoryId;

using Abstractions;
using Services;

public class GetMetadataByIdFeature : IFeature
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<GetMetadataRepository>();
        services.AddScoped<CategoryAttributeService>();
    }
}