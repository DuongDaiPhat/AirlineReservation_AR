namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class AircraftSeatConfig
    {
        public int ConfigId { get; set; }
        public int AircraftId { get; set; }
        public int SeatClassId { get; set; }
        public int SeatCount { get; set; }
        public int? RowStart { get; set; }
        public int? RowEnd { get; set; }

        public Aircraft Aircraft { get; set; } = null!;
        public SeatClass SeatClass { get; set; } = null!;
    }
}
