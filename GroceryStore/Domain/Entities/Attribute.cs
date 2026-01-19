namespace GroceryStore.Domain;

using GroceryStore.Shared;
using GroceryStore.Domain.Enums;

public class Attribute : BaseEntity
{
    public string Name { get; private set; }

    public AttributeDataType DataType { get; private set; }

    public string Unit { get; private set; }

    public int MinValue { get; private set; }

    public int MaxValue { get; private set; }

    public string? Value { get; private set; }


}