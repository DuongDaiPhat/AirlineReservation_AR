using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightResultDTO
    {
        public int FlightId { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }

        public string FromAirportCode { get; set; }
        public string ToAirportCode { get; set; }
        public string FromAirportName { get; set; }
        public string ToAirportName { get; set; }

        public DateTime FlightDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int DurationMinutes { get; set; }

        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }

        public string AircraftType { get; set; }

        // mới: ghế còn lại theo từng loại
        public Dictionary<string, int> SeatsLeftByClass { get; set; }
        public Dictionary<int, string> SeatClassesMap { get; set; }
        public string SelectedSeatClassName { get; set; }

        // mới: tổng số ghế trống
        public int TotalSeatsLeft { get; set; }
        public string FlightCode { get; set; }

    }



}
