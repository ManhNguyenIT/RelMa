using System.Text.Json.Serialization;

namespace RelMa.Shared.Abstractions.Query;

public abstract class PaginationQuery : BaseQuery
{
    [JsonPropertyName("page")]
    public int? Page { get; set; }
    [JsonPropertyName("pageSize")]
    public int? PageSize { get; set; }
}
