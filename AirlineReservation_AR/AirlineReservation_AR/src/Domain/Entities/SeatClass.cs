using System.Collections.Generic;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class SeatClass
    {
        public int SeatClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public decimal PriceMultiplier { get; set; }
        public int? BaggageAllowanceKg { get; set; }
        public int? CabinBaggageAllowanceKg { get; set; }
        public string? Description { get; set; }
        public string? Features { get; set; }

        public ICollection<AircraftSeatConfig> SeatConfigurations { get; set; } = new HashSet<AircraftSeatConfig>();
        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public ICollection<FlightPricing> FlightPricings { get; set; } = new HashSet<FlightPricing>();
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
