using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class UncheckedTicketDTO
    {
        public string BookingCode { get; set; }
        public string PassengerName { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }
        public string SeatClass { get; set; }
    }
}
