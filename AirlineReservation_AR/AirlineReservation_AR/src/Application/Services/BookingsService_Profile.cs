using AirlineReservation_AR.src.AirlineReservation.Application.Services;
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
    public class BookingsService_Profile
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
        public async Task<string> CheckBookingStatusAsync(int bookingId)
        {
            using var db = DIContainer.CreateDb();
            var booking = await db.Bookings.FindAsync(bookingId);

            if (booking == null) return "Unknown";

            // Nếu đang Pending mà quá 1 tiếng -> Expired
            if (booking.Status == "Pending")
            {
                var timeDiff = DateTime.Now - booking.BookingDate;
                if (timeDiff.TotalHours >= 1)
                {
                    booking.Status = "Expired";
                    await db.SaveChangesAsync();
                    return "Expired";
                }
            }
            return booking.Status;
        }

        // Hàm mới: Xử lý sau khi thanh toán thành công
        public async Task<bool> ProcessPaymentSuccessAsync(int bookingId)
        {
            using var db = DIContainer.CreateDb();
            var booking = await db.Bookings
                .Include(b => b.BookingFlights)
                .Include(b => b.Passengers)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null) return false;

            // Kiểm tra lại lần cuối xem có bị hết hạn trong lúc đang thao tác không
            if ((DateTime.Now - booking.BookingDate).TotalHours >= 1)
            {
                booking.Status = "Expired";
                await db.SaveChangesAsync();
                return false; // Thanh toán thất bại do hết hạn
            }

            // 1. Chuyển trạng thái Booking -> Confirm
            booking.Status = "Confirmed"; // Hoặc "Confirm" tùy convention DB của bạn

            // 2. Tạo Ticket cho từng Passenger trong từng Flight
            // Lưu ý: Logic này giả định vé chưa được tạo lúc Booking. 
            // Nếu vé đã tạo sẵn ở trạng thái Pending, bạn chỉ cần update status (dùng code cũ của bạn).
            // Dưới đây là code tạo mới theo yêu cầu:

            var ticketService = new TicketService();
            // Cần sửa TicketService để hỗ trợ truyền DB context hoặc gọi logic nội bộ tương đương
            // Tuy nhiên để đảm bảo tính nhất quán transaction, ta nên viết logic tạo vé trực tiếp ở đây 
            // hoặc TicketService phải chấp nhận DbContext được truyền vào.

            // Cách đơn giản nhất viết trực tiếp logic tạo vé tại đây để dùng chung Transaction:
            foreach (var bf in booking.BookingFlights)
            {
                foreach (var passenger in booking.Passengers)
                {
                    // Kiểm tra xem vé đã tồn tại chưa để tránh trùng lặp
                    bool ticketExists = await db.Tickets.AnyAsync(t => t.BookingFlightId == bf.BookingFlightId && t.PassengerId == passenger.PassengerId);

                    if (!ticketExists)
                    {
                        var newTicket = new Ticket
                        {
                            TicketId = Guid.NewGuid(),
                            BookingFlightId = bf.BookingFlightId,
                            PassengerId = passenger.PassengerId,
                            SeatClassId = 1, // Cần logic lấy SeatClassId từ BookingFlight hoặc User selection
                            TicketNumber = $"TKT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                            Status = "Issued", // Yêu cầu đề bài
                            CheckedInAt = null
                        };
                        db.Tickets.Add(newTicket);
                    }
                    else
                    {
                        // Nếu vé đã có (dạng Pending) thì update lên Issued
                        var existingTickets = await db.Tickets
                            .Where(t => t.BookingFlightId == bf.BookingFlightId && t.PassengerId == passenger.PassengerId)
                            .ToListAsync();
                        foreach (var t in existingTickets) t.Status = "Issued";
                    }
                }
            }

            await db.SaveChangesAsync();
            return true;
        }
    }
}
