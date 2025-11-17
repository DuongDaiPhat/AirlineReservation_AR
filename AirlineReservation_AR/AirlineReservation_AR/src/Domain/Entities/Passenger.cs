using System;
using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public string? PassengerType { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? IdNumber { get; set; }

        public Booking Booking { get; set; } = null!;
        public ICollection<BookingService> BookingServices { get; set; } = new HashSet<BookingService>();
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
