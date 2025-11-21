using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class BookingRequest
    {
        public Guid UserId { get; set; }

        public FlightSelectionModel SelectedFlight { get; set; }

        public ContactInfoDTO Contact { get; set; }

        public List<PassengerDTO> Passengers { get; set; }
        public List<BaggageOptionDTO> Baggage { get; set; }

        public string TripType { get; set; } = "OneWay";

        public decimal TotalPrice { get; set; }
    }

}
