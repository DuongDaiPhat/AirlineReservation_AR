using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Airline
    {
        public int AirlineId { get; set; }
        public string AirlineName { get; set; } = null!;
        public string IataCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; }

        public Country Country { get; set; } = null!;
        public ICollection<Aircraft> Aircraft { get; set; } = new HashSet<Aircraft>();
        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }
}
