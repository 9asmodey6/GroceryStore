namespace GroceryStore.Infrastructure;

using System.Data;
using System.Text.Json;
using Dapper;

public class JsonTypeHandler : SqlMapper.TypeHandler<Dictionary<string, string>>
{
    // Как мы записываем словарь В базу
    public override void SetValue(IDbDataParameter parameter, Dictionary<string, string>? value)
    {
        parameter.Value = value == null
            ? DBNull.Value
            : JsonSerializer.Serialize(value);
    }

    // Как мы читаем ИЗ базы в словарь
    public override Dictionary<string, string>? Parse(object value)
    {
        if (value is string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        return null;
    }
}