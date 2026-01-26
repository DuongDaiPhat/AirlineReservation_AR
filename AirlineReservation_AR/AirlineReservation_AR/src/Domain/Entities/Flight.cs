using System;
using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int AirlineId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public int AircraftId { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public DateTime FlightDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int? DurationMinutes { get; set; }
        public string? Status { get; set; }
        public decimal BasePrice { get; set; }

        public Airline Airline { get; set; } = null!;
        public Aircraft Aircraft { get; set; } = null!;
        public Airport DepartureAirport { get; set; } = null!;
        public Airport ArrivalAirport { get; set; } = null!;
        public ICollection<FlightPricing> FlightPricings { get; set; } = new HashSet<FlightPricing>();
        public ICollection<BookingFlight> BookingFlights { get; set; } = new HashSet<BookingFlight>();

    }
}
