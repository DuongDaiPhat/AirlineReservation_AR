using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightSegmentContext
    {
        public FlightResultDTO Flight { get; set; }

        public Dictionary<string, ServiceOption> PassengerServices { get; set; }
            = new();

        public List<PassengerWithServicesDTO> PassengerBundles { get; set; }
            = new();

        public decimal TotalServicePrice { get; set; }
        public decimal TotalFlightPrice { get; set; }
    }
}
