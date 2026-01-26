using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightSearchResultDTO
    {
        public List<FlightDayPriceDTO> DayTabs { get; set; }
        public List<FlightDayPriceDTO> DayTabReturn { get; set; }
        public FlightResultDTO BestFlight { get; set; }
        public List<FlightResultDTO> AllFlights { get; set; }
        public List<AirlineFilterDTO> AirlineFilters { get; set; }
        public List<AirlineFilterDTO> RetrunAirlineFilters { get; set; }
        public List<FlightResultDTO> OutboundFlights { get; set; }
        public List<FlightResultDTO> ReturnFlights { get; set; }

        public bool RoundTrip { get; set; }
    }
}
