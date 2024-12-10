using SensorApi.Models.Enums;

namespace SensorApi.Models.DatabaseModels;

public class Sensor
{
    public Guid Id { get; set; } // Unique identifier for the sensor (GUID) 
    public string Name { get; set; } // Sensor name
    public string Location { get; set; } // Sensor location (e.g., room or area name)
    public int MinValue { get; set; } // Minimum measurable value
    public int MaxValue { get; set; } // Maximum measurable value
    public int CurrentValue { get; set; } // Latest value from the sensor
    public DateTime CreateDate { get; set; } // When the sensor was added to the system
    public DateTime? UpdateDate { get; set; } // When the sensor's data was last updated
    public SensorStatus Status { get; set; } // Sensor's operational status
    public bool IsSync { get; set; } // when its save on SQl its true;

}
