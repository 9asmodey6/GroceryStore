namespace GroceryStore.Infrastructure.Middlewares;

using Serilog.Context;

public class SerilogEnrichmentMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User?.FindFirst("sub")?.Value ?? "anonymous";
        var correlationId = context.TraceIdentifier;

        using (LogContext.PushProperty("UserId", userId))
        using (LogContext.PushProperty("CorrelationId", correlationId))
        using (LogContext.PushProperty("RequestPath", context.Request.Path))
        using (LogContext.PushProperty("RequestMethod", context.Request.Method))
        {
            await next(context);
        }
    }
}