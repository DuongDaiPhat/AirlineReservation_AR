using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class RevenueByAirlineDtoAdmin
    {
        public string AirlineName { get; set; } = string.Empty;
        public string IataCode { get; set; } = string.Empty;
        public decimal TotalRevenue { get; set; }
        public int FlightCount { get; set; }
        public int BookingCount { get; set; }
    }
}
