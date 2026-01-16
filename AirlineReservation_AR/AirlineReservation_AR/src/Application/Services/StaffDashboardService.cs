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

        public List<UncheckedTicketDTO> SearchUncheckedTickets(string keyword)
        {
            using var db = DIContainer.CreateDb();
            
            // Normalize keyword for search
            var searchTerm = string.IsNullOrWhiteSpace(keyword) 
                ? string.Empty 
                : keyword.Trim().ToLower();

            // Build query with proper joins
            var query = (from t in db.Tickets
                        join f in db.Flights on t.BookingFlightId equals f.FlightId
                        join p in db.Passengers on t.PassengerId equals p.PassengerId
                        where t.Status == "Issued"
                        select new
                        {
                            Ticket = t,
                            Flight = f,
                            Passenger = p
                        }).AsEnumerable(); // Load data first to avoid SQL translation issues

            // Apply search filter
            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    query = query.Where(x =>
            //        (x.Ticket.BookingCode != null && x.Ticket.BookingCode.ToLower().Contains(searchTerm)) ||
            //        (x.Passenger.FullName != null && x.Passenger.FullName.ToLower().Contains(searchTerm)) ||
            //        (x.Flight.FlightNumber != null && x.Flight.FlightNumber.ToLower().Contains(searchTerm)) ||
            //        (x.Ticket.BookingFlight != null && x.Ticket.BookingFlight.ToLower().Contains(searchTerm))
            //    );
            //}

            //// Project to DTO and order results
            //var results = query
            //    .OrderBy(x => x.Flight.DepartureTime)
            //    .Take(50)
            //    .Select(x => new UncheckedTicketDTO
            //    {
            //        BookingCode = x.Ticket.BookingCode,
            //        PassengerName = x.Passenger.FullName,
            //        FlightNumber = x.Flight.FlightNumber,
            //        DepartureDate = x.Flight.DepartureDate,
            //        FromAirport = x.Flight.FromAirportCode,
            //        ToAirport = x.Flight.ToAirportCode,
            //        SeatClass = x.Ticket.SeatClass
            //    })
            //    .ToList();

            return null;
        }
    }
}
