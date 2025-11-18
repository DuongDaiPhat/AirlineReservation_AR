using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context
{
    public class AirlineReservationDbContextFactory : IDesignTimeDbContextFactory<AirlineReservationDbContext>
    {
        public AirlineReservationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("AirlineReservationDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<AirlineReservationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AirlineReservationDbContext(optionsBuilder.Options);
        }
    }
}