using web_sensor_app.Models.Enums;

namespace web_sensor_app.Models.ResponseModels;

public class SensorResponse
{
    public Guid Id { get; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int CurrentValue { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public SensorStatus Status { get; set; }
    public bool IsSync { get; set; }
}
