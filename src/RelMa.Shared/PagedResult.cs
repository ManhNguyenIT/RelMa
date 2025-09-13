namespace RelMa.Shared;

public sealed class PagedResult<T>
{
    public int RowCount { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
    public T[]? Items { get; set; }
}
