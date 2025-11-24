using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class ReportSummaryDtoAdmin
    {
        public int TotalFlights { get; set; }
        public int TotalPassengers { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal CancellationRate { get; set; }
        public decimal AverageSatisfactionScore { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
