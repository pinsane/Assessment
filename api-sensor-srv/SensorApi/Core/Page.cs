namespace SensorApi.Core;
public interface IPage<TResult>
{
    public object Items { get; }

    int Index { get; }

    int Size { get; }

    int TotalCount { get; }

    int TotalPages { get; }

    bool HasPreviousPage { get; }

    bool HasNextPage { get; }
}
public class Page<TResult> : IPage<TResult>
{
    public object Items { get; }

    public int Index { get; }

    public int Size { get; }

    public int TotalCount { get; }

    public int TotalPages { get; }

    public bool HasPreviousPage { get; }

    public bool HasNextPage { get; }

    public static IPage<TResult> Empty => new Page<TResult>(null, 0, 0, 0);

    public Page(object items, int pageIndex, int pageSize, int totalItemCount)
    {
        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        if (totalItemCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalItemCount));
        }

        Items = items;
        Index = pageIndex;
        Size = pageSize;
        TotalCount = totalItemCount;
        TotalPages = (totalItemCount + pageSize - 1) / pageSize;
        HasNextPage = pageIndex < TotalPages;
        HasPreviousPage = pageIndex > 1;
        if (totalItemCount == 0)
        {
            TotalPages = 0;
        }
        else if (pageIndex <= 0 || pageIndex > TotalPages)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex));
        }
    }
}
