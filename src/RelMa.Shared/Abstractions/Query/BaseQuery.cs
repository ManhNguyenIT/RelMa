using System.Text.Json.Serialization;

namespace RelMa.Shared.Abstractions.Query;

public abstract class BaseQuery
{
    [JsonPropertyName("q")]
    public string? Q { get; set; }
    [JsonPropertyName("includes")]
    public string? Includes { get; set; }
    [JsonPropertyName("filters")]
    public string? Filters { get; set; }
    [JsonPropertyName("orders")]
    public string? Orders { get; set; }
    [JsonPropertyName("columns")]
    public string? Columns { get; set; }
}
