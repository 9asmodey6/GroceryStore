namespace GroceryStore.Shared.Models.Optional;

public readonly struct Optional<TValue>
{
    internal Optional(bool hasValue, TValue value)
    {
        HasValue = hasValue;
        Value = value;
    }

    public bool HasValue { get; }
    public TValue? Value { get; }

    public static Optional<TValue> None => default;
    public static Optional<TValue> Some(TValue? value) => new (true, value);
}