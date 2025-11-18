//using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Shared.Utils
{
    internal class Connection
    {
        private static readonly string _connStr = "Server=ADMIN-PC;Database=demo;User Id=sa;Password=12345;TrustServerCertificate=True;";

        public static AirlineReservationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AirlineReservationDbContext>()
                .UseSqlServer(_connStr)
                .Options;

            return new AirlineReservationDbContext(options);
        }
    }
}
