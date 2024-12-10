namespace SensorApi.Models.Enums;

public enum SensorStatus
{
    Inactive = 1 << 0,
    Active = 1 << 1,
    Error = 1 << 2,
    Maintenance = 1 << 3,
}
