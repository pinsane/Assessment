using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_sensor_app.Business;
using web_sensor_app.Core;
using web_sensor_app.Models;
using web_sensor_app.Models.RequestModels;
using web_sensor_app.Models.ResponseModels;
using web_sensor_app.Models.ViewModels;

namespace web_sensor_app.Controllers;

public class SensorsController(ILogger<SensorsController> logger, ISensorService sensorService) : Controller
{
    private readonly ILogger<SensorsController> _logger = logger;
    private readonly ISensorService _sensorService = sensorService;
    private const string TempSensorsKey = "TemporarySensors";

    public async Task<IActionResult> Index(PagedRequest request)
    {
        var sensors = await _sensorService.GetAllSensorsAsync(request);

        return View(sensors);
    }



    [HttpPost]
    [Route("Sensor/SaveTemporary")]
    public async Task<IActionResult> SaveTemporary([FromBody] AddSensorRequest newRequests)
    {
        if (newRequests == null || newRequests.Requests.Count == 0)
        {
            return BadRequest("No sensors received.");
        }
        var sensors = await _sensorService.AddSensor(newRequests);



        return Ok("Sensors saved successfully.");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
