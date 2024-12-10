using web_sensor_app.Core;
using web_sensor_app.Models.ResponseModels;

namespace web_sensor_app.Models.ViewModels;

public class SensorViewModel
{
    public Page<SensorResponse> Sensors { get; set; }
}
