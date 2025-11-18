using System;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public int BookingFlightId { get; set; }
        public int PassengerId { get; set; }
        public int SeatClassId { get; set; }
        public string TicketNumber { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? Fees { get; set; }
        public string? Status { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public string? SeatNumber { get; set; }

        public BookingFlight BookingFlight { get; set; } = null!;
        public Passenger Passenger { get; set; } = null!;
        public SeatClass SeatClass { get; set; } = null!;
    }
}
