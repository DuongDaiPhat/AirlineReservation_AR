using System.Collections.Generic;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class Country
    {
        public string CountryCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string? Currency { get; set; }
        public bool IsActive { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();
        public ICollection<Airline> Airlines { get; set; } = new HashSet<Airline>();
    }
}
