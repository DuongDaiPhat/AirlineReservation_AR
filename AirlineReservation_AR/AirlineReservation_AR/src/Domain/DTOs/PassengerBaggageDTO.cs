using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PassengerBaggageDTO
    {
        public int PassengerIndex { get; set; }
        public int ServiceType { get; set; }
        public string Weight { get; set; }        // "20kg", "30kg"
        public decimal Price { get; set; }
    }
}
