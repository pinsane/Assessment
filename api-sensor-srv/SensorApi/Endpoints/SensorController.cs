using System.Net;
using Microsoft.AspNetCore.Mvc;
using SensorApi.Business;
using SensorApi.Business.Publishers;
using SensorApi.Core;
using SensorApi.Extensions;
using SensorApi.Models.RequestModels;

namespace SensorApi.Endpoints;

/// <summary>
/// Controller for managing sensor data.
/// </summary>
[Route("v1/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly ISensorSqlService _sensorSqlService;
    private readonly ISensorService _sensorService;
    private readonly IFilteredQuery _filteredQuery;
    private readonly ISaveItemToSqlDbEventPublisher _saveItemToSqlDbEventPublisher;

    /// <summary>
    /// Initializes a new instance of the SensorController.
    /// </summary>
    /// <param name="sensorSqlService">Service for SQL-based sensor operations.</param>
    /// <param name="sensorService">Service for managing sensor-related operations.</param>
    /// <param name="filteredQuery">Service for applying filtered queries.</param>
    /// <param name="saveItemToSqlDbEventPublisher">Publisher for saving items to the SQL database.</param>
    public SensorController(ISensorSqlService sensorSqlService, ISensorService sensorService, IFilteredQuery filteredQuery, ISaveItemToSqlDbEventPublisher saveItemToSqlDbEventPublisher)
    {
        _sensorSqlService = sensorSqlService;
        _sensorService = sensorService;
        _filteredQuery = filteredQuery;
        _saveItemToSqlDbEventPublisher = saveItemToSqlDbEventPublisher;
    }

    /// <summary>
    /// Endpoint to add new sensors to the system.
    /// </summary>
    /// <param name="request">The request object containing the list of sensors to be added.</param>
    /// <returns>A task that represents the asynchronous operation. Returns an HTTP response with the added sensors or an error message.</returns>
    [HttpPost]
    public async Task<ActionResult> AddSensors([FromBody] AddSensorRequest request)
    {
        try
        {
            // Call the SensorService to add the sensors to the database and retrieve the result.
            // The AddSensors method from the _sensorService is invoked, passing the incoming request.
            // It will return the added sensors (an array of Sensor objects).
            var result = await _sensorService.AddSensors(request);
            await _saveItemToSqlDbEventPublisher.Publish(result);

            // Return an HTTP 200 OK response with the result (the added sensors).
            // This uses the Result extension method to structure the response.
            // The first argument is the result (added sensors), 
            // the second is a success message, and the default error code is 0 (indicating no error).
            return this.Result(result, "Sensors added successfully");
        }
        catch (Exception ex)
        {
            // If an exception occurs during the execution, catch it and return an HTTP response indicating failure.
            // The response will contain the error message and a custom error code (500 for internal server error).
            return this.Result(null, "Failed to add sensors", 500, ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a paginated list of all sensors.
    /// </summary>
    /// <param name="request">The paginated request parameters.</param>
    /// <returns>An ActionResult containing the paginated list of sensors.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] PagedRequest request)
    {
        try
        {
            // Get the query for all sensors from the SQL service
            var query = _sensorSqlService.GetAll(request);

            // Apply pagination and retrieve the result
            var result = await _filteredQuery.ToPageListAsync(query, request);

            // Return the result using the Result extension method, with a success message
            return this.Result(result, "Sensors retrieved successfully");
        }
        catch (Exception ex)
        {
            // In case of an error, return a failure response using the Result extension method
            return this.Result(null, "Failed to retrieve sensors", 500, ex.Message);
        }
    }
}
