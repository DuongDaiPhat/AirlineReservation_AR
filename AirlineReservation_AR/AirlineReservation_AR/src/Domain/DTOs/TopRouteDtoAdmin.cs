using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class TopRouteDtoAdmin
    {
        public int Rank { get; set; }
        public string Route { get; set; } = string.Empty;
        public string DepartureCode { get; set; } = string.Empty;
        public string ArrivalCode { get; set; } = string.Empty;
        public int BookingCount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal MarketShare { get; set; }
    }
}
