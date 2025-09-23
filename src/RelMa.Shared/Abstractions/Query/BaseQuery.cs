namespace RelMa.Shared.Abstractions.Query;

public abstract class BaseQuery
{
    public string? Q { get; set; }
    public string? Includes { get; set; }
    public string? Filters { get; set; }
    public string? Orders { get; set; }
    public string? Columns { get; set; }
}
