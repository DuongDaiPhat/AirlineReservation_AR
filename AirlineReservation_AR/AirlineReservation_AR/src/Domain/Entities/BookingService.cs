namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class BookingService
    {
        public int BookingServiceId { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public int? PassengerId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Booking Booking { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public Passenger? Passenger { get; set; }
    }
}
