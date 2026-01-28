using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class AircraftSelectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalSeats { get; set; }
    }
}
