namespace GroceryStore.Shared.Models;

public class NormalizationResult<T>
{
    required public bool IsSuccess { get; init; }

    public T? Value { get; init; }

    required public ValidationResult Validation { get; init; }

    public static NormalizationResult<T> Success(T value)
    {
        return new () { IsSuccess = true, Value = value, Validation = new ValidationResult() };
    }

    public static NormalizationResult<T> Fail(ValidationResult vr)
    {
        return new () { IsSuccess = false, Value = default(T), Validation = vr };
    }
}