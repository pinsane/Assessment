using SensorApi.Business;
using SensorApi.Business.Publishers;
using SensorApi.Core;

namespace SensorApi.ServiceRegistrations;

/// <summary>
/// Provides extension methods to register services in the dependency injection container.
/// </summary>
public static class RegisterServices
{
    /// <summary>
    /// Registers the application services in the DI container.
    /// This method adds scoped services for sensor management, database interaction, and event publishing.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="configuration">The application's configuration to be used for service registration.</param>
    /// <returns>The IServiceCollection with the services added.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the ISensorService interface to be resolved by SensorService
        services.AddScoped<ISensorService, SensorService>()

        // Register the ISensorSqlService interface to be resolved by SensorSqlService
        .AddScoped<ISensorSqlService, SensorSqlService>()

        // Register the ISaveItemToSqlDbEventPublisher interface to be resolved by SaveItemToSqlDbEventPublisher
        .AddScoped<ISaveItemToSqlDbEventPublisher, SaveItemToSqlDbEventPublisher>()

        // Register the IFilteredQuery interface to be resolved by FilteredQuery
        .AddScoped<IFilteredQuery, FilteredQuery>();

        // Return the updated service collection to allow method chaining
        return services;
    }
}
