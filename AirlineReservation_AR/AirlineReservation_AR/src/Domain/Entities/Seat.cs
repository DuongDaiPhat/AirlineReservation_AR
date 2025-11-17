namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int AircraftId { get; set; }
        public int SeatClassId { get; set; }
        public string SeatNumber { get; set; } = null!;
        public bool IsAvailable { get; set; }

        public Aircraft Aircraft { get; set; } = null!;
        public SeatClass SeatClass { get; set; } = null!;
    }
}
