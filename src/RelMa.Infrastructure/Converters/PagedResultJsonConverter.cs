using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RelMa.Infrastructure.Converters;

internal sealed class PagedResultJsonConverter<T> : JsonConverter<PagedResult<T>>
{
    public override PagedResult<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var temp = JsonSerializer.Deserialize<PagedResultSurrogate>(ref reader, options)
            ?? throw new JsonException("Failed to deserialize PagedResultSurrogate");

        return new PagedResult<T>
        {
            Queryable = temp.Items?.AsQueryable() ?? Enumerable.Empty<T>().AsQueryable(),
            RowCount = temp.RowCount,
            PageSize = temp.PageSize,
            PageCount = temp.PageCount,
            CurrentPage = temp.CurrentPage
        };
    }

    public override void Write(Utf8JsonWriter writer, PagedResult<T> value, JsonSerializerOptions options)
    {
        var surrogate = new PagedResultSurrogate
        {
            Items = value.Queryable?.ToList() ?? [],
            RowCount = value.RowCount,
            PageSize = value.PageSize,
            PageCount = value.PageCount,
            CurrentPage = value.CurrentPage
        };

        JsonSerializer.Serialize(writer, surrogate, options);
    }

    private sealed class PagedResultSurrogate
    {
        public required List<T> Items { get; set; }
        public int RowCount { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}