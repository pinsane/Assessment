using Microsoft.EntityFrameworkCore;
using SensorApi.Models.DatabaseModels;

namespace SensorApi.ServiceRegistrations;

/// <summary>
/// Provides extension methods to register database contexts in the dependency injection container.
/// </summary>
public static class RegisterDatabases
{
    /// <summary>
    /// Registers the database contexts for MariaDB and SQL Server with connection settings from the configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="configuration">The application's configuration to be used for service registration.</param>
    /// <returns>The IServiceCollection with the database contexts added.</returns>
    public static IServiceCollection AddDataBases(this IServiceCollection services, IConfiguration configuration)
    {
        // Register MariaDbContext with MySQL as the database provider
        services
            .AddDbContext<MariaDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("MariaDb"), // Get the MariaDB connection string from configuration
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MariaDb")) // Automatically detect the server version
                ))

            // Register SqlDbContext with SQL Server as the database provider
            .AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlDb"), // Get the SQL Server connection string from configuration
                    x =>
                    {
                        // Configure SQL Server retry on failure settings (5 retries with a 10-second delay)
                        x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                ));

        // Return the updated service collection to allow method chaining
        return services;
    }
}
