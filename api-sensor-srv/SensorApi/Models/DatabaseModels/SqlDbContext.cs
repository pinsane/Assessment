using Microsoft.EntityFrameworkCore;
namespace SensorApi.Models.DatabaseModels;

public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
    public DbSet<Sensor> Sensors { get; set; }

}
