namespace GroceryStore.Bootstrap.Filters;

using FluentValidation;

public class ValidationFilter<TRequest>(IValidator<TRequest> validator, ILogger<ValidationFilter<TRequest>> logger)
    : IEndpointFilter
    where TRequest : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestName = typeof(TRequest).Name;
        var request = context.Arguments.OfType<TRequest>().FirstOrDefault();

        if (request is null)
        {
            logger.LogWarning(
                "Validation failed: Request object of type {RequestType} was not found in the context",
                requestName);
            return TypedResults.BadRequest("Request object not found.");
        }

        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));

            logger.LogWarning(
                "Validation failed for {RequestType}. Errors: {ValidationErrors}",
                requestName,
                errors);

            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        logger.LogInformation("Validation for {RequestType} passed successfully", requestName);

        return await next(context);
    }
}