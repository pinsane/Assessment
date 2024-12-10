using Events;
using MassTransit;

namespace SensorApi.Business.Consumer;

/// <summary>
/// Consumer for handling the SaveItemToSqlDbEvent.
/// This class listens for events that indicate new sensor data has been added 
/// and processes the event by saving the sensor data to the SQL database.
/// </summary>
public class SaveItemToSqlDbEventConsumer : IConsumer<SaveItemToSqlDbEvent>
{
    private readonly ISensorSqlService _sensorSqlService;
    private readonly ILogger<SaveItemToSqlDbEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveItemToSqlDbEventConsumer"/> class.
    /// </summary>
    /// <param name="sensorSqlService">The service used to interact with the sensor data in the database.</param>
    /// <param name="logger">The logger to record logs during event consumption.</param>
    public SaveItemToSqlDbEventConsumer(ISensorSqlService sensorSqlService, ILogger<SaveItemToSqlDbEventConsumer> logger)
    {
        _sensorSqlService = sensorSqlService;
        _logger = logger;
    }

    /// <summary>
    /// Consumes the SaveItemToSqlDbEvent message and processes the event by saving the sensor data to the database.
    /// </summary>
    /// <param name="context">The consume context that provides access to the event message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<SaveItemToSqlDbEvent> context)
    {
        try
        {
            // Call the AddSensors method to save the sensors from the event message to the database.
            await _sensorSqlService.AddSensors(context.Message.Sensors);
        }
        catch (Exception e)
        {
            // If an error occurs, log the exception with the relevant error message and the user ID (if available).
            _logger.LogError(e, "An unhandled exception occurred while processing the SaveItemToSqlDbEvent.");
        }
    }
}
