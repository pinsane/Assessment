using Microsoft.EntityFrameworkCore;
using SensorApi.Models.DatabaseModels;
using SensorApi.Models.RequestModels;

namespace SensorApi.Business;

/// <summary>
/// Service for managing sensor-related business logic.
/// </summary>
public interface ISensorService
{
    /// <summary>
    /// Adds a collection of new sensors to the database(MariaDB).
    /// </summary>
    /// <param name="request">The request containing sensor data to be added.</param>
    /// <returns>A task that represents the asynchronous operation, containing the added sensors.</returns>
    Task<Sensor[]> AddSensors(AddSensorRequest request);
}

/// <summary>
/// Implementation of the ISensorService interface to handle sensor-related operations.
/// </summary>
public class SensorService : ISensorService
{
    private readonly MariaDbContext _db;

    // Constructor injection to get the instance of the database context
    public SensorService(MariaDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Adds multiple sensors to the database based on the provided request.
    /// </summary>
    /// <param name="request">The request object containing an array of sensor data.</param>
    /// <returns>A task that represents the asynchronous operation, containing the added sensors.</returns>
    public async Task<Sensor[]> AddSensors(AddSensorRequest request)
    {
        if (request == null || request.requests == null || request.requests.Length == 0)
        {
            return null;
        }

        // Map the request data to sensor entities
        var sensors = request.requests.Select(r => new Sensor
        {
            Id = r.Id,
            Name = r.Name,                       // Sensor name
            Location = r.Location,               // Sensor location
            MinValue = r.MinValue,               // Minimum sensor value
            MaxValue = r.MaxValue,               // Maximum sensor value
            CurrentValue = r.CurrentValue,       // Current value reading
            CreateDate = DateTime.UtcNow,        // Set the creation date to current UTC time
            UpdateDate = null,                   // Initially, no update date is set
            Status = r.Status,                   // Sensor status
            IsSync = false,                      // Initially set IsSync to false (not synchronized)
        }).ToArray();

        // Add the sensors to the database context
        _db.Sensors.AddRange(sensors);

        // Save changes to the database asynchronously
        await _db.SaveChangesAsync();

        // Return the added sensors
        return sensors;
    }
}
