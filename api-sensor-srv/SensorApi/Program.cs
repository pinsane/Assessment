using Microsoft.EntityFrameworkCore;
using SensorApi.Models.DatabaseModels;
using SensorApi.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

// Add endpoints API explorer to generate API documentation for endpoints
builder.Services.AddEndpointsApiExplorer();

// Register custom services, databases, bus, and Swagger configurations
builder.Services.RegisterServices(builder.Configuration);

// Add support for controllers in the application
builder.Services.AddControllers();

// Build the application
var app = builder.Build();

// Ensure the database is migrated to the latest version upon startup
using (var serviceScope = app.Services.CreateScope())
{
    // Get the SqlDbContext service to apply migrations for SQL Server
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<SqlDbContext>();
    dbContext.Database.Migrate();  // Applies any pending migrations to the SQL Server database

    // Get the MariaDbContext service to apply migrations for MariaDB
    var mariaDbContext = serviceScope.ServiceProvider.GetRequiredService<MariaDbContext>();
    mariaDbContext.Database.Migrate();  // Applies any pending migrations to the MariaDB database
}



// Enable developer exception page for detailed error messages (typically for development environment)
app.UseDeveloperExceptionPage();

// Enable Swagger middleware for API documentation generation
app.UseSwagger();

// Enable Swagger UI for interacting with the API documentation
app.UseSwaggerUI(options =>
{
    // Set the endpoint for Swagger UI to fetch the API documentation from
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MariaDbSrv V1");

    // Optional: Set Swagger UI as the default page for the application
    options.RoutePrefix = string.Empty; 
});

// Setup routing for incoming HTTP requests
app.UseRouting();

// Map controller endpoints to routes
app.MapControllers();
app.UseCors("CorsPolicy");

// Start the application and listen for incoming requests
app.Run();