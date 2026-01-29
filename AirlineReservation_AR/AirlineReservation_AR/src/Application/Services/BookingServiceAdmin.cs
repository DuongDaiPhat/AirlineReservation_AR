using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingServiceAdmin : IBookingServiceAdmin
    {
        public async Task<IEnumerable<BookingDtoAdmin>> GetAllBookingsAsync()
        {
            using var db = DIContainer.CreateDb();

            var bookings = await db.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Aircraft)
                            .ThenInclude(a => a.AircraftType)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Tickets)
                        .ThenInclude(t => t.SeatClass)
                .Include(b => b.Payments)
                .AsNoTracking()
                .ToListAsync();

            return bookings.Select(MapToDto).ToList();
        }

        public async Task<BookingDtoAdmin?> GetBookingByReferenceAsync(string bookingReference)
        {
            using var db = DIContainer.CreateDb();

            var booking = await db.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Aircraft)
                            .ThenInclude(a => a.AircraftType)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Tickets)
                        .ThenInclude(t => t.SeatClass)
                .Include(b => b.Payments)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

            return booking != null ? MapToDto(booking) : null;
        }

        public async Task<IEnumerable<BookingDtoAdmin>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            using var db = DIContainer.CreateDb();

            var bookings = await db.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Aircraft)
                            .ThenInclude(a => a.AircraftType)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Tickets)
                        .ThenInclude(t => t.SeatClass)
                .Include(b => b.Payments)
                .Where(b => b.BookingDate.Date >= startDate.Date && b.BookingDate.Date <= endDate.Date)
                .AsNoTracking()
                .ToListAsync();

            return bookings.Select(MapToDto).ToList();
        }

        public async Task<IEnumerable<BookingDtoAdmin>> FilterBookingsAsync(BookingFilterDto filter)
        {
            using var db = DIContainer.CreateDb();

            var query = db.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Aircraft)
                            .ThenInclude(a => a.AircraftType)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Tickets)
                        .ThenInclude(t => t.SeatClass)
                .Include(b => b.Payments)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.BookingReference))
                query = query.Where(b => b.BookingReference.Contains(filter.BookingReference));

            if (!string.IsNullOrWhiteSpace(filter.EmailOrPhone))
            {
                var searchTerm = filter.EmailOrPhone;
                query = query.Where(b =>
                    (b.ContactEmail != null && b.ContactEmail.Contains(searchTerm)) ||
                    (b.ContactPhone != null && b.ContactPhone.Contains(searchTerm)) ||
                    (b.User != null && b.User.FullName.Contains(searchTerm)));
            }

            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                query = query.Where(b => b.BookingDate.Date >= filter.StartDate.Value.Date &&
                                        b.BookingDate.Date <= filter.EndDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(filter.BookingStatus))
                query = query.Where(b => b.Status == filter.BookingStatus);

            if (!string.IsNullOrWhiteSpace(filter.PaymentStatus))
                query = query.Where(b => b.Payments.Any(p => p.Status == filter.PaymentStatus));

            var bookings = await query.AsNoTracking().ToListAsync();
            return bookings.Select(MapToDto).ToList();
        }

        public async Task<bool> ConfirmPaymentAsync(string bookingReference)
        {
            using var db = DIContainer.CreateDb();

            var booking = await db.Bookings
                .Include(b => b.Payments)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

            if (booking == null) return false;

            var payment = booking.Payments?.FirstOrDefault();
            if (payment == null) return false;

            payment.Status = "Completed";
            payment.CompletedAt = DateTime.Now;
            booking.Status = "Confirmed";

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBookingAsync(string bookingReference)
        {
            using var db = DIContainer.CreateDb();

            var booking = await db.Bookings
                .Include(b => b.Payments)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

            if (booking == null) return false;

            booking.Status = "Cancelled";

            var payment = booking.Payments?.FirstOrDefault();
            if (payment != null && payment.Status == "Completed")
            {
                payment.RefundedAmount = payment.Amount;
            }

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBookingCustomerAsync(string bookingReference, string fullName, string phone, string email)
        {
             using var db = DIContainer.CreateDb();
             var booking = await db.Bookings.Include(b => b.User).FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
             
             if (booking == null) return false;

             // If booking has linked user, updates might need to be on User entity or just Booking contact info?
             // Assuming Booking entity has contact fields. Logic: Update Booking Contact Info.
             // If User is linked, we generally Update the User info OR just the booking specific contact info.
             // Based on DTO, we have "CustomerName, ContactEmail, ContactPhone".
             // CustomerName comes from User.FullName. So we might need to update User if exists.

             if (booking.User != null)
             {
                 booking.User.FullName = fullName;
                 // Note: Updating User phone/email might affect login or other bookings. 
                 // Safest is to update Booking's snapshots if they exist, or Users if that's the requirement.
                 // Given the request "Edit Customer Info", typically means fixing typos for THIS booking or updating contact.
                 // Let's update User properties if linked, AND Booking properties if they act as snapshots.
                 
                 // However, Booking entity definition in Step 1852 shows:
                 // ContactEmail = booking.ContactEmail
                 // ContactPhone = booking.ContactPhone
                 // CustomerName = booking.User?.FullName
                 
                 // So we update:
                 booking.ContactEmail = email;
                 booking.ContactPhone = phone;
                 booking.User.FullName = fullName;
             }
             else
             {
                 // No user linked? (Guest booking logic if applicable, but code shows User inclusion).
                 // If User is null, we can't update name unless Booking has guest name field.
                 // DTO says: CustomerName = booking.User?.FullName ?? "N/A"
                 // So if User is null, we can't update Name.
                 // Let's assume User is always present for now based on 'Include(b=>b.User)'.
                 
                 booking.ContactEmail = email;
                 booking.ContactPhone = phone;
             }

             await db.SaveChangesAsync();
             return true;
        }

        private BookingDtoAdmin MapToDto(Booking booking)
        {
            var bookingFlight = booking.BookingFlights?.FirstOrDefault();
            var flight = bookingFlight?.Flight;
            var payment = booking.Payments?.FirstOrDefault();
            var ticket = bookingFlight?.Tickets?.FirstOrDefault();

            return new BookingDtoAdmin
            {
                BookingId = booking.BookingId,
                BookingReference = booking.BookingReference,
                CustomerName = booking.User?.FullName ?? "N/A",
                ContactEmail = booking.ContactEmail ?? "",
                ContactPhone = booking.ContactPhone ?? "",
                BookingDate = booking.BookingDate,
                Status = booking.Status ?? "Unknown",
                PassengerCount = booking.Passengers?.Count ?? 0,
                TotalAmount = payment?.Amount ?? 0,
                PaymentMethod = payment?.PaymentMethod ?? "N/A",
                PaymentStatus = payment?.Status ?? "Pending",
                FlightInfo = new FlightInfoDtoAdmin
                {
                    FlightNumber = flight?.FlightNumber ?? "N/A",
                    DepartureAirport = flight?.DepartureAirport?.AirportName ?? "N/A",
                    ArrivalAirport = flight?.ArrivalAirport?.AirportName ?? "N/A",
                    FlightDate = flight?.FlightDate ?? DateTime.MinValue,
                    DepartureTime = flight?.DepartureTime ?? TimeSpan.Zero,
                    ArrivalTime = flight?.ArrivalTime ?? TimeSpan.Zero,

                    // ✅ ĐÚNG: Lấy AircraftName từ Aircraft
                    AircraftName = flight?.Aircraft?.AircraftName ?? "N/A",

                    // ✅ ĐÚNG: Lấy TypeName từ AircraftType (ưu tiên DisplayName)
                    AircraftType = flight?.Aircraft?.AircraftType?.DisplayName ??
                                  flight?.Aircraft?.AircraftType?.TypeName ??
                                  "N/A",

                    // ✅ ĐÚNG: Lấy ClassName từ SeatClass của Ticket (ưu tiên DisplayName)
                    SeatClass = ticket?.SeatClass?.DisplayName ??
                               ticket?.SeatClass?.ClassName ??
                               "Economy"
                }
            };
        }
    }
}