namespace GroceryStore.Shared.Models;

public class NormalizationResult<T>
{
    required public bool IsSucces { get; init; }

    public T? Value { get; init; }

    required public ValidationResult Validation { get; init; }

    public static NormalizationResult<T> Success(T value)
    {
        return new () { IsSucces = true, Value = value, Validation = new ValidationResult() };
    }

    public static NormalizationResult<T> Fail(ValidationResult vr)
    {
        return new () { IsSucces = false, Value = default(T), Validation = vr };
    }
}