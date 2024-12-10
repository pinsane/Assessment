using web_sensor_app.Business;

namespace web_sensor_app.ServiceRegistrations;

public static class RegisterServices
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddHttpContextAccessor();
        services.AddHttpClient<ISensorService, SensorService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ServiceEndPoints:Sensors"]);
        });
        return services;
    }
}
