namespace GroceryStore.Application;

using Abstractions;
using ServiceScan.SourceGenerator;

public static partial class DependencyInjection
{
    [GenerateServiceRegistrations(AssignableTo = typeof(IEndpoint), CustomHandler = "MapEndpoint")]
    public static partial IEndpointRouteBuilder MapEndpointsGenerated(this IEndpointRouteBuilder endpoints);

    [GenerateServiceRegistrations(AssignableTo = typeof(IFeature), CustomHandler = "ConfigureServices")]
    public static partial IServiceCollection AddFeaturesGenerated(this IServiceCollection services);
}