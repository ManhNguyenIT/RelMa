namespace RelMa.Shared.Abstractions.Query;

public abstract class PaginationQuery : BaseQuery
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}
