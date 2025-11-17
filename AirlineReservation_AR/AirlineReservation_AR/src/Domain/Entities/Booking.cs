using System;
using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Status { get; set; }
        public string? Currency { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? SpecialRequests { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Passenger> Passengers { get; set; } = new HashSet<Passenger>();
        public ICollection<BookingFlight> BookingFlights { get; set; } = new HashSet<BookingFlight>();
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public ICollection<BookingService> BookingServices { get; set; } = new HashSet<BookingService>();
        public ICollection<BookingPromotion> BookingPromotions { get; set; } = new HashSet<BookingPromotion>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
    }
}
