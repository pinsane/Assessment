namespace web_sensor_app.ServiceRegistrations;

public static class Registration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServices(configuration)
        ;
        services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy",
                   builder => builder
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials());
           });

        return services;
    }
}
