namespace GroceryStore.Infrastructure;

using System.Data;
using System.Text.Json;
using Dapper;

public class JsonMetadataMapper : SqlMapper.TypeHandler<Dictionary<int, string>>
{
    public override void SetValue(IDbDataParameter parameter, Dictionary<int, string>? value)
    {
        parameter.Value = value == null
            ? DBNull.Value
            : JsonSerializer.Serialize(value);
    }

    public override Dictionary<int, string>? Parse(object value)
    {
        if (value is string json)
        {
            return JsonSerializer.Deserialize<Dictionary<int, string>>(json);
        }

        return null;
    }
}