namespace RelMa.Shared.Abstractions.Query;

public abstract class PaginationQuery : BaseQuery
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
