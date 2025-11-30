using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class StaffDashboardService : IStaffDashboardService
    {
        public StaffDashboardStatsDTO GetTodayStats(Guid staffId)
        {
            using var db = DIContainer.CreateDb();

            var today = DateTime.Now.Date;

            // TODO: đổi lại tên bảng/cột cho đúng với DB của bạn
            // Ví dụ giả định:
            //  - Booking: có AssignedStaffId, CreatedAt, Status (string)
            //  - Issue:   có AssignedStaffId, CreatedAt

            var bookingsToday = db.Bookings
                .Where(b => b.UserId == staffId &&
                            b.BookingDate >= today);

            //var issuesToday = db.StaffIssues
            //    .Where(i => i.AssignedStaffId == staffId &&
            //                i.CreatedAt >= today);

            int totalTickets = bookingsToday.Count();
            int completed = bookingsToday.Count(b => b.Status == "CheckedIn");
            int inProgress = bookingsToday.Count(b => b.Status == "InProgress");

            return new StaffDashboardStatsDTO
            {
                TicketsProcessed = totalTickets,
                CheckinCompleted = completed,
                InProgress = inProgress,
                //ReportedIssues = issuesToday.Count(),
                TotalTicketsToday = totalTickets,
                CompletedTicketsToday = completed
            };
        }
    }
}
