using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class BaggageOptionDTO
    {
        public int ServiceId { get; set; }
        public string Label { get; set; } // "20kg"
        public decimal Price { get; set; }
    }
}
