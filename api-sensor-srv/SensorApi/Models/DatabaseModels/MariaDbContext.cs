using Microsoft.EntityFrameworkCore;
namespace SensorApi.Models.DatabaseModels;

public class MariaDbContext(DbContextOptions<MariaDbContext> options) : DbContext(options)
{

    public DbSet<Sensor> Sensors { get; set; }


}
