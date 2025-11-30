using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IStaffDashboardService
    {
        // Lấy thống kê trong ngày cho dashboard của nhân viên
        StaffDashboardStatsDTO GetTodayStats(Guid staffId);

        // Tìm kiếm các vé chưa check-in
        List<UncheckedTicketDTO> SearchUncheckedTickets(string keyword);
    }
}
