using System.Reflection;
using Microsoft.OpenApi.Models;

namespace SensorApi.ServiceRegistrations;

/// <summary>
/// Provides an extension method to register Swagger services in the dependency injection container.
/// </summary>
public static class RegisterSwagger
{
    /// <summary>
    /// Registers Swagger services to enable automatic API documentation generation.
    /// This method configures Swagger to provide API documentation, including XML comments for better clarity.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The IServiceCollection with Swagger services added.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // Add Swagger generation services
        services.AddSwaggerGen(c =>
        {
            // Define Swagger document with title and version
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MariaDbSrv", Version = "v1" });

            // Set up XML comments if available for additional documentation
            var xmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

            // Check if the XML file exists, then include it in the Swagger documentation
            if (File.Exists(xmlFile))
            {
                c.IncludeXmlComments(xmlFile);
            }
        });

        // Return the updated services collection to allow method chaining
        return services;
    }
}
