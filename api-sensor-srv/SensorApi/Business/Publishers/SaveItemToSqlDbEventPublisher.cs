using Events;
using MassTransit;
using SensorApi.Models.DatabaseModels;

namespace SensorApi.Business.Publishers;

/// <summary>
/// Publisher for the SaveItemToSqlDbEvent. 
/// This class is responsible for publishing an event when new sensor data 
/// needs to be saved to the SQL database.
/// </summary>
public interface ISaveItemToSqlDbEventPublisher
{
    /// <summary>
    /// Publishes the SaveItemToSqlDbEvent to the bus, which contains sensor data to be saved to the database.
    /// </summary>
    /// <param name="request">The sensor data to be published in the event.</param>
    Task Publish(Sensor[] request);
}

/// <summary>
/// Concrete implementation of ISaveItemToSqlDbEventPublisher that uses MassTransit to publish events.
/// </summary>
public class SaveItemToSqlDbEventPublisher : ISaveItemToSqlDbEventPublisher
{
    private readonly IBusControl _bus;

    /// <summary>
    /// Initializes a new instance of the SaveItemToSqlDbEventPublisher class.
    /// </summary>
    /// <param name="bus">The MassTransit bus used for publishing events.</param>
    public SaveItemToSqlDbEventPublisher(IBusControl bus)
    {
        _bus = bus;
    }

    /// <summary>
    /// Publishes the SaveItemToSqlDbEvent containing the provided sensor data to the MassTransit bus.
    /// </summary>
    /// <param name="request">The sensor data to be included in the event.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Publish(Sensor[] request)
    {
        // Publish the event to the bus, encapsulating the sensor data in the SaveItemToSqlDbEvent message.
        await _bus.Publish(new SaveItemToSqlDbEvent
        {
            Sensors = request,
        });
    }
}
