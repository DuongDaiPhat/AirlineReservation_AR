using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string AirportName { get; set; } = null!;
        public string IataCode { get; set; } = null!;
        public string CityCode { get; set; } = null!;
        public bool IsActive { get; set; }

        public City City { get; set; } = null!;
        public ICollection<Flight> DepartingFlights { get; set; } = new HashSet<Flight>();
        public ICollection<Flight> ArrivingFlights { get; set; } = new HashSet<Flight>();
    }
}
