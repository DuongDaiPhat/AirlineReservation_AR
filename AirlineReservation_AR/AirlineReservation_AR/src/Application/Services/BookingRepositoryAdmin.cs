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
    public class BookingRepositoryAdmin : IBookingRepositoryAdmin
    {
        private readonly AirlineReservationDbContext _context;

        public BookingRepositoryAdmin(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .FirstOrDefaultAsync(b => b.BookingId == id);
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
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Bookings
                .Include(b => b.Payments)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Payments)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Where(b => b.BookingDate.Date >= startDate.Date && b.BookingDate.Date <= endDate.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByStatusAsync(string status)
        {
            return await _context.Bookings
                .Include(b => b.Payments)
                .Where(b => b.Status == status)
                .ToListAsync();
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string bookingReference)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingReference == bookingReference);
        }
    }
}
