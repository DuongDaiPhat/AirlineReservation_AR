using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.Entities;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class FareRuleService : IFareRuleService
    {
        public async Task<FareRule?> GetFareRuleForFlightAsync(int flightId, int seatClassId)
        {
            using var db = DIContainer.CreateDb();

            // Tìm FlightPricing khớp Flight và SeatClass, sau đó lấy FareRule
            var pricing = await db.FlightPricings
                .Include(fp => fp.FareRule)
                .FirstOrDefaultAsync(fp => fp.FlightId == flightId && fp.SeatClassId == seatClassId);

            return pricing?.FareRule;
        }

        public async Task<FareRule?> GetFareRuleForTicketAsync(Guid ticketId)
        {
            using var db = DIContainer.CreateDb();

            // Truy vết: Ticket -> BookingFlight -> FlightPricing (cần match SeatClass của ticket) -> FareRule
            // Lưu ý: Đây là truy vấn phức tạp, ta load Ticket trước để lấy thông tin
            var ticket = await db.Tickets
                .Include(t => t.BookingFlight)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null || ticket.BookingFlight == null) return null;

            // Tìm Pricing áp dụng cho chuyến bay cũ và hạng ghế của vé đó
            var pricing = await db.FlightPricings
                .Include(fp => fp.FareRule)
                .FirstOrDefaultAsync(fp => fp.FlightId == ticket.BookingFlight.FlightId
                                        && fp.SeatClassId == ticket.SeatClassId);

            return pricing?.FareRule;
        }

        public async Task<bool> CanChangeAsync(Guid ticketId)
        {
            using var db = DIContainer.CreateDb();

            var ticket = await db.Tickets
                .Include(t => t.BookingFlight)
                .ThenInclude(bf => bf.Flight) // Load Flight để lấy giờ bay
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null || ticket.BookingFlight?.Flight == null) return false;

            // Lấy FareRule (giữ nguyên logic cũ)
            var pricing = await db.FlightPricings
                .Include(fp => fp.FareRule)
                .FirstOrDefaultAsync(fp => fp.FlightId == ticket.BookingFlight.FlightId
                                        && fp.SeatClassId == ticket.SeatClassId);
            var rule = pricing?.FareRule;

            DateTime flightDate = ticket.BookingFlight.Flight.FlightDate;     // Kiểu DateTime
            TimeSpan flightTime = ticket.BookingFlight.Flight.DepartureTime;  // Kiểu TimeSpan

            // Phép cộng trong C#: DateTime + TimeSpan = DateTime mới
            DateTime fullDepartureTime = flightDate + flightTime;

            // Tính khoảng cách thời gian
            var hoursDifference = (fullDepartureTime - DateTime.Now).TotalHours;

            if (hoursDifference < rule.MinHoursBeforeChange) return false;

            return true;
        }
    }
}
