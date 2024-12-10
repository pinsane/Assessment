using Microsoft.VisualBasic;
using web_sensor_app.Core;
using web_sensor_app.Models.RequestModels;
using web_sensor_app.Models.ResponseModels;

namespace web_sensor_app.Business;
public interface ISensorService
{
    Task<Page<SensorResponse>> GetAllSensorsAsync(PagedRequest request);
    Task<SensorResponse[]> AddSensor(AddSensorRequest request);
}

public class SensorService(HttpClient httpClient) : ISensorService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Page<SensorResponse>> GetAllSensorsAsync(PagedRequest request)
    {

        var result = await _httpClient.GetAsync<Page<SensorResponse>>($"v1/Sensor", request.ToQueryString());
        return result;
    }
    public async Task<SensorResponse[]> AddSensor(AddSensorRequest request)
    {

        var result = await _httpClient.PostAsJsonAsync<SensorResponse[]>($"v1/Sensor", request);
        return result;
    }
}
