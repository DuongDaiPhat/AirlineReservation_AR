using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class DashboardStatisticsDtoAdmin
    {
        public decimal TotalRevenue { get; set; }
        public int TotalBookings { get; set; }
        public int TotalFlights { get; set; }
        public int TotalCustomers { get; set; }
        public decimal AverageTicketPrice { get; set; }
        public decimal RevenueGrowthRate { get; set; }
        public decimal BookingGrowthRate { get; set; }
        public decimal FlightGrowthRate { get; set; }
        public decimal CustomerGrowthRate { get; set; }
        public decimal PriceChangeRate { get; set; }
    }
}
