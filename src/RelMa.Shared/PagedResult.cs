namespace RelMa.Shared;

public sealed class PagedResult<T>
{
    public int RowCount { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
    public bool HasNextPage => PageCount > 0 && CurrentPage < PageCount;
    public bool HasPreviousPage => PageCount > 0 && CurrentPage > 1;
    public T[]? Items { get; set; }
}
