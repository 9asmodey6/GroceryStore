namespace GroceryStore.Shared.Models;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class OptionalJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Optional<>);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var t = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(OptionalJsonConverter<>).MakeGenericType(t);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private sealed class OptionalJsonConverter<T> : JsonConverter<Optional<T>>
    {
        public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // ВАЖНО: если поле присутствует, конвертер вызовется.
            // Если поле отсутствует — конвертер не вызовется, и останется default => HasValue=false (то, что нам нужно).
            if (reader.TokenType == JsonTokenType.Null)
            {
                return Optional<T>.Some(default!);
            }

            var value = JsonSerializer.Deserialize<T>(ref reader, options);
            return Optional<T>.Some(value!);
        }

        public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                // обычно не сериализуют None вообще, но на всякий случай:
                writer.WriteNullValue();
                return;
            }

            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}