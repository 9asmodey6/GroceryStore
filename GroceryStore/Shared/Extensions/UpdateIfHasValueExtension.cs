namespace GroceryStore.Shared.Extensions;

using Features.Admin.Product.UpdateProduct;
using Models;

public static class UpdateExtensions
{
    // Overload for structs, to avoid nullable type in non-nullable methods.
    public static void UpdateIfHasValue<TTarget, TValue>(
        this TTarget target,
        Optional<TValue?> optional,
        Action<TTarget, TValue> setter)
        where TTarget : class
        where TValue : struct
    {
        if (optional.HasValue && optional.Value.HasValue)
        {
            setter(target, optional.Value.Value);
        }
    }

    // Overload for strings. Checking for an empty string.
    public static void UpdateIfHasValue<TTarget>(
        this TTarget target,
        Optional<string?> optional,
        Action<TTarget, string> setter)
        where TTarget : class
    {
        if (optional.HasValue && !string.IsNullOrWhiteSpace(optional.Value))
        {
            setter(target, optional.Value);
        }
    }

    // Base overload.
    public static void UpdateIfHasValue<TTarget, TValue>(
        this TTarget target,
        Optional<TValue> optional,
        Action<TTarget, TValue> setter)
        where TTarget : class
    {
        if (optional is { HasValue: true, Value: not null })
        {
            setter(target, optional.Value);
        }
    }
}