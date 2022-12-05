using Microsoft.EntityFrameworkCore;
using WhifferSnifferAPI.Models;

#nullable disable

namespace WhifferSnifferAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<SensorConfiguration> SensorConfigurations { get; set; }

        public DbSet<SensorReading> SensorReadings { get; set; }
    }
}
