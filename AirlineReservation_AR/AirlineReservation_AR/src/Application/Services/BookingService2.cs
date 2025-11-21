using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingService2 : IBookingService
    {
        private readonly AirlineReservationDbContext _context;

        public BookingService2(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllWithDetailsAsync()
        {
            return await _context.Bookings
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
                        .ThenInclude(f => f.Airline)
                .Include(b => b.Payments)
                .Include(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .Include(b => b.BookingServices)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<Booking?> GetByReferenceAsync(string bookingReference)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.Payments)
                .Include(b => b.BookingServices)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            try
            {
                _context.Bookings.Update(booking);

                if (booking.Payments != null)
                    foreach (var p in booking.Payments)
                        _context.Payments.Update(p);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _context.Bookings
                .Where(b => b.BookingDate >= from && b.BookingDate <= to)
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return await GetAllWithDetailsAsync();

            keyword = keyword.ToLower();
            return await _context.Bookings
                .Where(b =>
                    b.BookingReference.ToLower().Contains(keyword) ||
                    b.ContactEmail.ToLower().Contains(keyword) ||
                    b.ContactPhone.Contains(keyword))
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .ToListAsync();
        }
    }
}
