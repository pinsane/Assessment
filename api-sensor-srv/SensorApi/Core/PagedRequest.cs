namespace SensorApi.Core;
public class PagedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public OrderType Order { get; set; } 

    public string? Search { get; set; }
}
public enum OrderType
{
    Asc,
    Desc
}

