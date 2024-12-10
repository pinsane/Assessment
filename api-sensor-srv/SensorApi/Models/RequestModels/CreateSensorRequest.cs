using SensorApi.Models.Enums;

namespace SensorApi.Models.RequestModels;

public class CreateSensorRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int CurrentValue { get; set; }
    public SensorStatus Status { get; set; }
}
