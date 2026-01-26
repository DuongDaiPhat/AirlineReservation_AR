using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class BookingFlight
    {
        public int BookingFlightId { get; set; }
        public int BookingId { get; set; }
        public int FlightId { get; set; }

        public Booking Booking { get; set; } = null!;
        public Flight Flight { get; set; } = null!;
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
        public ICollection<BookingService> BookingServices { get; set; } = new HashSet<BookingService>();
        public string Status { get; set; } = "Booked";
    }
}
