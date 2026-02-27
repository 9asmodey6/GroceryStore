namespace GroceryStore.Shared.Models;

public struct Optional<TValue>
{
    public Optional(TValue value)
    {
        HasValue = true;
        Value = value;
    }

    public bool HasValue { get; }
    public TValue Value { get; }
}