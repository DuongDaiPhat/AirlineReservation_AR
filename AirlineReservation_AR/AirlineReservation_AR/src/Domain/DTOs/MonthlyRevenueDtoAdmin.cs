using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class MonthlyRevenueDtoAdmin
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Revenue { get; set; }
        public int BookingCount { get; set; }
        public string MonthLabel { get; set; } = string.Empty;
    }
}
