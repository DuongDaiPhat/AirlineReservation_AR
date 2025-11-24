using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingsService
    {
        public async Task<List<Booking>> GetBookingsByUserAsync(Guid userId)
        {
            using var db = DIContainer.CreateDb();

            return await db.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.BookingFlights)
                .ThenInclude(bf => bf.Flight)
                    .ThenInclude(da => da.DepartureAirport)
                        .ThenInclude(a => a.City)

            .Include(b => b.BookingFlights)
                .ThenInclude(bf => bf.Flight)
                    .ThenInclude(f => f.ArrivalAirport)
                        .ThenInclude(a => a.City)
            .Include(b => b.BookingFlights)
                .ThenInclude(bf => bf.Tickets)
                    .ThenInclude(t => t.Passenger)
            .Include(b => b.BookingFlights)
                .ThenInclude(bf => bf.Flight)
                    .ThenInclude(f => f.Airline)
            .ToListAsync();
        }

        public async Task<bool> ConfirmBookingAsync(int bookingId)
        {
            using var db = DIContainer.CreateDb();

            // Lấy booking và bao gồm cả các vé (tickets) liên quan
            var booking = await db.Bookings
                .Include(b => b.BookingFlights)
                .ThenInclude(bf => bf.Tickets)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null) return false;

            booking.Status = "Confirmed";

            // 2. Cập nhật trạng thái tất cả các Ticket -> Issued
            foreach (var flight in booking.BookingFlights)
            {
                foreach (var ticket in flight.Tickets)
                {
                    ticket.Status = "Issued";
                }
            }

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(int bookingId, Guid userId)
        {
            using var db = DIContainer.CreateDb();

            var booking = await db.Bookings
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Tickets)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId && b.UserId == userId);

            if (booking == null)
                return false;

            // Chỉ cho hủy booking còn Pending / Confirm (tùy rule của em)
            if (!booking.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase) &&
                !booking.Status.Equals("Confirm", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            booking.Status = "Cancelled";

            foreach (var bf in booking.BookingFlights)
            {
                foreach (var t in bf.Tickets)
                {
                    t.Status = "Cancelled";
                }
            }

            await db.SaveChangesAsync();
            return true;
        }
    }
}
