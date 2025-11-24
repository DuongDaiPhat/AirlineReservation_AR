using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class ComprehensiveReportDtoAdmin
    {
        public DashboardStatisticsDtoAdmin Statistics { get; set; } = new();
        public List<MonthlyRevenueDtoAdmin> MonthlyRevenue { get; set; } = new();
        public List<TopRouteDtoAdmin> TopRoutes { get; set; } = new();
        public List<TopCustomerDtoAdmin> TopCustomers { get; set; } = new();
        public List<BookingStatusDtoAdmin> BookingStatuses { get; set; } = new();
        public ReportSummaryDtoAdmin Summary { get; set; } = new();
    }
}
