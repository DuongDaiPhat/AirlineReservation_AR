using AirlineReservation_AR.src.Domain.Entities;
using System;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class FlightPricing
    {
        public int PricingId { get; set; }
        public int FlightId { get; set; }
        public int SeatClassId { get; set; }
        public decimal Price { get; set; }
        public int BookedSeats { get; set; }

        public Flight Flight { get; set; } = null!;
        public SeatClass SeatClass { get; set; } = null!;

        public int FareRuleId { get; set; }
        public FareRule FareRule { get; set; } = null!;

    }
}
