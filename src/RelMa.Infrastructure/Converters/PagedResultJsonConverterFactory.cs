using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RelMa.Infrastructure.Converters;

public sealed class PagedResultJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        typeToConvert.GetGenericTypeDefinition() == typeof(PagedResult<>);

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var itemType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(PagedResultJsonConverter<>).MakeGenericType(itemType);
        return Activator.CreateInstance(converterType) as JsonConverter
            ?? throw new JsonException($"Failed to create converter for {typeToConvert}");
    }
}