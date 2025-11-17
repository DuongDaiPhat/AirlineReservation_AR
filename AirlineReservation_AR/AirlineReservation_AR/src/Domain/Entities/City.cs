using System.Collections.Generic;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class City
    {
        public string CityCode { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public bool IsActive { get; set; }

        public Country Country { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Airport> Airports { get; set; } = new HashSet<Airport>();
    }
}
