using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class StaffDashboardController
    {
        private readonly IStaffDashboardService _service;

        public StaffDashboardController(IStaffDashboardService service)
        {
            _service = service;
        }

        public StaffDashboardStatsDTO GetTodayStats(Guid staffId)
            => _service.GetTodayStats(staffId);

        public List<UncheckedTicketDTO> SearchUncheckedTickets(string keyword)
            => _service.SearchUncheckedTickets(keyword);
    }
}
