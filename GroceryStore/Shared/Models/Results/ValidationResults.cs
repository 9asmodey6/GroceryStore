namespace GroceryStore.Shared.Models.Results;

using GroceryStore.Shared.Enums;
using GroceryStore.Shared.Extensions;

public sealed class ValidationError
{
    public required string Code { get; init; }

    public required string Message { get; init; }

    public string? Field { get; init; }

    public int? ValueId { get; init; }
}

public sealed class ValidationResult
{
    private readonly List<ValidationError> _errors = new();

    public bool IsValid => _errors.Count == 0;

    public IReadOnlyList<ValidationError> Errors => _errors;

    public void Add(ValidationError error) => _errors.Add(error);

    public void Add(string code, string message, string? field = null, int? valueId = null)
    {
        _errors.Add(new ValidationError
        {
            Code = code,
            Message = message,
            Field = field,
            ValueId = valueId,
        });
    }

    public void Add(MetadataValidationErrorCode code, string message, string? field = null, int? valueId = null)
    {
        _errors.Add(new ValidationError
        {
            Code = code.ToApiCode(),
            Message = message,
            Field = field,
            ValueId = valueId,
        });
    }

    public void Merge(ValidationResult other)
    {
        if (!other.IsValid)
        {
            _errors.AddRange(other._errors);
        }
    }
}