namespace GroceryStore.Shared.Extensions.Logging;

using GroceryStore.Infrastructure.Middlewares;

public static class SerilogEnrichmentMiddlewareExtensions
{
    public static IApplicationBuilder UseSerilogEnrichment(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SerilogEnrichmentMiddleware>();
    }
}
