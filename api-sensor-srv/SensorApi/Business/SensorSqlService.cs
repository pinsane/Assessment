using SensorApi.Core;
using SensorApi.Models.DatabaseModels;

namespace SensorApi.Business
{
    /// <summary>
    /// Service for handling SQL operations related to sensors.
    /// </summary>
    public interface ISensorSqlService
    {
        /// <summary>
        /// Retrieves all sensors with optional search functionality for pagination.
        /// </summary>
        /// <param name="request">The request object containing pagination and search parameters.</param>
        /// <returns>An IQueryable of sensors, potentially filtered by search criteria.</returns>
        IQueryable<Sensor> GetAll(PagedRequest request);

        /// <summary>
        /// Adds an array of sensors to the database.
        /// </summary>
        /// <param name="request">An array of sensors to be added to the database.</param>
        /// <returns>A task that represents the asynchronous operation, returning a boolean indicating success.</returns>
        Task<bool> AddSensors(Sensor[] request);
    }

    /// <summary>
    /// Implementation of ISensorSqlService to handle data operations on the SQL database for sensors.
    /// </summary>
    public class SensorSqlService : ISensorSqlService
    {
        private readonly SqlDbContext _db;

        // Constructor to inject the SqlDbContext for database operations
        public SensorSqlService(SqlDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves all sensors from the database with optional search filters applied.
        /// </summary>
        /// <param name="request">The request containing the pagination and search criteria.</param>
        /// <returns>An IQueryable of sensors that matches the given search criteria.</returns>
        public IQueryable<Sensor> GetAll(PagedRequest request)
        {
            IQueryable<Sensor> query = _db.Sensors;
            // Apply search filter if provided in the request
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(m =>
                    m.Name.ToLower().Contains(request.Search.ToLower()) ||
                    m.Location.ToLower().Contains(request.Search.ToLower())
                );
            }
            return query; // Return the filtered query
        }

        /// <summary>
        /// Adds an array of sensors to the database and saves the changes.
        /// </summary>
        /// <param name="request">An array of Sensor objects to be added to the database.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the sensors were added successfully, otherwise false.</returns>
        public async Task<bool> AddSensors(Sensor[] request)
        {
            // Add the sensors to the database context
            _db.Sensors.AddRange(request);

            // Save the changes asynchronously and return whether the save was successful
            return await _db.SaveChangesAsync() > 0;
        }
    }
}