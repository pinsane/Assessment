namespace SensorApi.ServiceRegistrations;

/// <summary>
/// Provides an extension method to register all required services and configurations.
/// This method registers essential services like Swagger, databases, bus, and custom services.
/// </summary>
public static class Registration
{
    /// <summary>
    /// Registers all necessary services to the application's dependency injection container.
    /// This includes setting up Swagger for API documentation, adding database configurations, 
    /// setting up messaging (bus) services, and registering custom business services.
    /// </summary>
    /// <param name="services">The IServiceCollection to register the services into.</param>
    /// <param name="configuration">The application's configuration settings.</param>
    /// <returns>The updated IServiceCollection with all the services registered.</returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Set the console window title based on the entry assembly's name
        Console.Title = $"{System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name} service";

        // Register various services, configurations, and third-party libraries
        services
            // Register Swagger for API documentation
            .AddSwagger()

            // Register custom application services
            .AddServices(configuration)

            // Register database contexts and connections
            .AddDataBases(configuration)

            // Register the bus (rabbitMQ) for event publishing and consuming
            .AddBus(configuration);

        // Return the updated IServiceCollection to allow method chaining
        return services;
    }
}
