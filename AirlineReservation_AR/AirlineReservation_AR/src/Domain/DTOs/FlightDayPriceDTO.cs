using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightDayPriceDTO
    {
        public DateTime Date { get; set; }
        public decimal LowestPrice { get; set; }
    }
}
