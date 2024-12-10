namespace web_sensor_app.Models.Enums;

[Flags]
public enum SensorStatus 
{
    Inactive = 1 << 0,
    Active = 1 << 1,
    Error = 1 << 2,
    Maintenance = 1 << 3,
}
