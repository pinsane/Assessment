using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SensorApi.ServiceRegistrations;

/// <summary>
/// Provides extension methods to register and configure MassTransit and health checks in the DI container.
/// </summary>
public static class RegisterBus
{
    /// <summary>
    /// Registers MassTransit with RabbitMQ as the transport and configures health check options for readiness.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="configuration">The application's configuration to be used for service registration.</param>
    /// <returns>The IServiceCollection with the MassTransit and health check services added.</returns>
    public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration)
    {
        // Add MassTransit and configure it to use RabbitMQ
        services.AddMassTransit(x =>
        {
            // Get the entry assembly for the application (used for automatically registering consumers)
            var entryAssembly = Assembly.GetEntryAssembly();

            // Register all consumers from the entry assembly
            x.AddConsumers(entryAssembly);

            // Configure RabbitMQ as the transport for MassTransit
            x.UsingRabbitMq((context, cfg) =>
            {

                // Configure the RabbitMQ host with the connection details
                cfg.Host(configuration["Rabitmq:Host"],configuration["Rabitmq:VirtualHost"], h =>
                {
                    // Set the username and password for connecting to RabbitMQ
                    h.Username(configuration["Rabitmq:UserName"]);
                    h.Password(configuration["Rabitmq:PassWord"]);
                });

                // Automatically configure endpoints for consumers
                cfg.ConfigureEndpoints(context);
            });
        });

        // Configure health checks for the application
        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            // Set the delay between health check publishing attempts
            options.Delay = TimeSpan.FromSeconds(5);

            // Set the predicate to only include checks with the "ready" tag
            options.Predicate = (check) => check.Tags.Contains("ready");
        });

        // Return the updated service collection
        return services;
    }
}
