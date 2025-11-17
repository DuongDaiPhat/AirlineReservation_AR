using System.Collections.Generic;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class Aircraft
    {
        public int AircraftId { get; set; }
        public int AirlineId { get; set; }
        public int AircraftTypeId { get; set; }
        public string? AircraftName { get; set; }

        public Airline Airline { get; set; } = null!;
        public AircraftType AircraftType { get; set; } = null!;
        public ICollection<AircraftSeatConfig> SeatConfigurations { get; set; } = new HashSet<AircraftSeatConfig>();
        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
    }
}
