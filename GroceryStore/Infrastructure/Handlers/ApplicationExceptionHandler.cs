namespace GroceryStore.Infrastructure.Handlers;

using System.Security.Claims;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Exceptions;

public class ApplicationExceptionHandler(
    ILogger<ApplicationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken ct = default)
    {
        var userIdentifier = context.User.Identity?.IsAuthenticated == true
            ? context.User.FindFirstValue(ClaimTypes.Email) ?? context.User.Identity.Name
            : "anonymous";

        var (details, logLevel) = exception switch
        {
            UnauthorizedAccessException ex =>
                (HandleUnauthorized(ex, context), LogLevel.Warning),

            ForbiddenAccessException ex =>
                (HandleForbidden(ex, context), LogLevel.Warning),

            SecurityTokenException ex =>
                (HandleSecurityToken(ex, context), LogLevel.Error),

            _
                =>
                (HandleUnknownError(exception, context), LogLevel.Error)
        };

        logger.Log(
            logLevel,
            exception,
            "Exception occurred for user {UserIdentifier}: {ExceptionType} - {Message}",
            userIdentifier,
            exception.GetType().Name,
            exception.Message);

        context.Response.StatusCode = details.Status ?? StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(details, ct);

        return true;
    }


    private ProblemDetails HandleUnauthorized(
        UnauthorizedAccessException ex,
        HttpContext httpContext)
    {
        var result = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Title = "Unauthorized",
            Status = StatusCodes.Status401Unauthorized,
            Detail = "Authentication is required to access this resource. " +
                     "Please ensure that you have provided a valid JWT token in the Authorization header.",
            Instance = httpContext.Request.Path,
        };

        result.Extensions.Add("traceId", httpContext.TraceIdentifier);
        result.Extensions["actions"] = new
        {
            login = "/api/v1/login",
            register = "/api/v1/register",
        };

        return result;
    }

    private ProblemDetails HandleForbidden(
        Exception ex,
        HttpContext httpContext)
    {
        var result = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            Title = "Forbidden",
            Status = StatusCodes.Status403Forbidden,
            Detail = "You do not have permission to access this resource. " +
                     "This action requires specific role privileges.",
            Instance = httpContext.Request.Path,
        };

        result.Extensions.Add("traceId", httpContext.TraceIdentifier);
        result.Extensions["requiredRole"] = "Admin";

        return result;
    }

    private ProblemDetails HandleUnknownError(
        Exception ex,
        HttpContext httpContext)
    {
        var result = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An unexpected error occurred while processing your request.",
            Instance = httpContext.Request.Path,
        };

        result.Extensions.Add("traceId", httpContext.TraceIdentifier);

        return result;
    }

    private ProblemDetails HandleSecurityToken(
        Exception ex,
        HttpContext httpContext)
    {
        var result = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Invalid Token",
            Status = StatusCodes.Status401Unauthorized,
            Detail = ex.Message,
            Instance = httpContext.Request.Path,
        };

        result.Extensions.Add("traceId", httpContext.TraceIdentifier);

        return result;
    }
}