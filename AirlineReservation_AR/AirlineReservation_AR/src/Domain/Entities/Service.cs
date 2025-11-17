using System.Collections.Generic;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; }

        public ICollection<BookingService> BookingServices { get; set; } = new HashSet<BookingService>();
    }
}
