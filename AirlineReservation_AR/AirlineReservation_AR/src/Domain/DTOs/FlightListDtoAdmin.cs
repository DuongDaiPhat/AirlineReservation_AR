using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightListDtoAdmin
    {
        public int FlightId { get; set; }
        public string FlightCode { get; set; } = string.Empty;

        public string Route { get; set; } = string.Empty; // SGN → HAN
        public string Airline { get; set; } = string.Empty;
        public string Aircraft { get; set; } = string.Empty;
        public DateTime FlightDate { get; set; }

        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public decimal BasePrice { get; set; }
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public int AvailableSeats => TotalSeats - BookedSeats;

        public string Status { get; set; } = string.Empty;
    }
}
