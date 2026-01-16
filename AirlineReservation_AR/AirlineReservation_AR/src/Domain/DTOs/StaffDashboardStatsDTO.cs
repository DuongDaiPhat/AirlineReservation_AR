using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class StaffDashboardStatsDTO
    {
        public int TicketsProcessed { get; set; }
        public int CheckinCompleted { get; set; }
        public int InProgress { get; set; }
        public int ReportedIssues { get; set; }

        // Nếu cần thêm cho progress bar
        public int TotalTicketsToday { get; set; }
        public int CompletedTicketsToday { get; set; }
    }
}
