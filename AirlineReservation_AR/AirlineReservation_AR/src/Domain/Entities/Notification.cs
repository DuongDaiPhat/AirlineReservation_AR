using System;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Channel { get; set; } = null!;
        public int? RelatedBookingId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public Booking? RelatedBooking { get; set; }
    }
}
