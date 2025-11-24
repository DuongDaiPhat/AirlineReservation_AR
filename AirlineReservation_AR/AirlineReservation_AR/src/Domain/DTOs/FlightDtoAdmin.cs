using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightDtoAdmin
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public string AirlineName { get; set; } = null!;
        public string AircraftModel { get; set; } = null!;
        public string DepartureAirport { get; set; } = null!;
        public string DepartureCity { get; set; } = null!;
        public string DepartureIataCode { get; set; } = null!;
        public string ArrivalAirport { get; set; } = null!;
        public string ArrivalCity { get; set; } = null!;
        public string ArrivalIataCode { get; set; } = null!;
        public DateTime FlightDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int? DurationMinutes { get; set; }
        public string? Status { get; set; }
        public decimal BasePrice { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
    }
}
