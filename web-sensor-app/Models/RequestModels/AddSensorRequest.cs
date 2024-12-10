namespace web_sensor_app.Models.RequestModels;

public class AddSensorRequest
{
    public List<CreateSensorRequest> Requests { get; set; } = [];

}
