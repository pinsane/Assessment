using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SensorApi.Business;
using SensorApi.Models.RequestModels;
using SensorApi.Core;
using SensorApi.Models.Enums;
using SensorApi.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;

public class SensorServiceTests
{
    private readonly MariaDbContext _dbContext;
    private readonly SensorService _sensorService;

    public SensorServiceTests()
    {
        // Set up an in-memory database for testing
        var options = new DbContextOptionsBuilder<MariaDbContext>()
                        .UseInMemoryDatabase(databaseName: "TestDb") // Use an in-memory database
                        .Options;

        _dbContext = new MariaDbContext(options); // Use the in-memory database context

        // Ensure the database is created
        _dbContext.Database.EnsureCreated();

        _sensorService = new SensorService(_dbContext);
    }

    [Fact]
    public async Task AddSensors_ShouldAddSensors()
    {
        // Arrange: Set up the AddSensorRequest with a valid sensor
        var request = new AddSensorRequest
        {
            requests = new[]
            {
                new CreateSensorRequest
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Sensor",
                    Location = "Test Location",
                    MinValue = 0,
                    MaxValue = 100,
                    CurrentValue = 50,
                    Status = SensorStatus.Active
                }
            }
        };

        // Act: Call the method under test
        var result = await _sensorService.AddSensors(request);

        // Assert: Verify that the correct sensor is returned
        Assert.Single(result);  // Ensure only one sensor was added
        Assert.Equal("Test Sensor", result[0].Name);  // Verify the sensor's name
        Assert.Equal("Test Location", result[0].Location);  // Verify the sensor's location
        Assert.Equal(SensorStatus.Active, result[0].Status);  // Verify the sensor's status

        // Verify that the sensor was added to the in-memory database
        Assert.Contains(result[0], _dbContext.Sensors);
    }

    [Fact]
    public async Task AddSensors_ShouldReturnEmpty_WhenRequestIsNull()
    {
        // Arrange: Set up an invalid AddSensorRequest (null)
        AddSensorRequest request = null;

        // Act: Call the method under test
        var result = await _sensorService.AddSensors(request);

        // Assert: Ensure no sensors are returned
        Assert.Null(result);
    }

    [Fact]
    public async Task AddSensors_ShouldReturnEmpty_WhenRequestsIsEmpty()
    {
        // Arrange: Set up an AddSensorRequest with an empty array
        var request = new AddSensorRequest { requests = [] };

        // Act: Call the method under test
        var result = await _sensorService.AddSensors(request);

        // Assert: Ensure no sensors are returned
        Assert.Null(result);
    }

    [Fact]
    public async Task AddSensors_ShouldReturnBadRequest_WhenRequiredFieldsAreMissing()
    {
        // Arrange: Set up an invalid request (Name is missing, Location is missing)
        var request = new AddSensorRequest
        {
            requests =
            [
            new CreateSensorRequest
            {
                Id = Guid.NewGuid(),
                Name = "",  // Invalid: Name is empty (required field)
                Location = "", // Invalid: Location is empty (required field)
                MinValue = 0,
                MaxValue = 100,
                CurrentValue = 50,
                Status = SensorStatus.Active
            }
        ]
        };

        // Act: Call the method under test
        var result = await _sensorService.AddSensors(request);

        // Assert: Check that the result is a BadRequest with the appropriate message
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);  // Verify it's a BadRequest result
        Assert.Contains("The Name field is required.", actionResult.Value.ToString()); // Check that the error message includes "Name field is required"
        Assert.Contains("The Location field is required.", actionResult.Value.ToString()); // Check that the error message includes "Location field is required"
    }


}