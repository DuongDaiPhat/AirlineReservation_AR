using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightSelectionModel
    {
        public int FlightId { get; set; }
        public int SeatClassId { get; set; }

        public DateTime FlightDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public decimal Price { get; set; }

        public string FromCode { get; set; }
        public string ToCode { get; set; }

        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
    }

}
