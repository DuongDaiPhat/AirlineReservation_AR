using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class AvailableFlightDto
    {
        // === BASIC FLIGHT INFO ===
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        // === AIRLINE INFO ===
        public string AirlineName { get; set; } = null!;
        public string AirlineLogo { get; set; } = null!;

        // === ROUTE INFO ===
        public string FromAirportCode { get; set; } = null!;
        public string ToAirportCode { get; set; } = null!;

        // === PRICING ===
        public decimal Price { get; set; }
        public string FareRuleCode { get; set; } = null!;

        // === SEAT INFO ===
        public int TotalSeatsLeft { get; set; } = 0;

        // key: "Economy", "Business", "First"
        public Dictionary<string, int> SeatsLeftByClass { get; set; }
            = new Dictionary<string, int>();
    }
}
