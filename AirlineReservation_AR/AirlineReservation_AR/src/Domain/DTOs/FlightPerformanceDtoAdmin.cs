using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightPerformanceDtoAdmin
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public decimal OccupancyRate { get; set; }
        public decimal Revenue { get; set; }
    }
}
