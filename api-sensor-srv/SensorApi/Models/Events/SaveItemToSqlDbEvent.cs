using MassTransit;
using SensorApi.Models.DatabaseModels;
namespace Events;

[ExcludeFromTopology]

public class SaveItemToSqlDbEvent
{
    public required Sensor[] Sensors { get; set; }

}