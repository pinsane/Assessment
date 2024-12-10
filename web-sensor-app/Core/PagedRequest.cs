namespace web_sensor_app.Core
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public OrderType Order { get; set; } = OrderType.Desc;
        public string? Search { get; set; }
    }
    public enum OrderType
    {
        Asc,
        Desc
    }
}