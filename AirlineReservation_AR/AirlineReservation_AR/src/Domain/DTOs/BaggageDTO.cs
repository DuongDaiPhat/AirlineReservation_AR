using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class BaggageDTO
    {
        public string Weight { get; set; }
        public decimal Price { get; set; }
        public int ServiceType { get; set; }
    }
}
