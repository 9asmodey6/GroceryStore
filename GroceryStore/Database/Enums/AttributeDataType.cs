namespace GroceryStore.Database.Enums;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AttributeDataType
{
    String = 1,
    Integer = 2,
    Decimal = 3,
    DateTime = 4,
    Boolean = 5,
}