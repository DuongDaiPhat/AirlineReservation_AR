using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class BookingFlightService : IBookingFlightService
    {
        private readonly AirlineReservationDbContext _context;

        public BookingFlightService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingFlight?> GetByIdAsync(int bookingFlightId)
        {
            return await _context.BookingFlights
                .FirstOrDefaultAsync(bf => bf.BookingFlightId == bookingFlightId);
        }

        public async Task<BookingFlight?> GetByIdWithDetailsAsync(int bookingFlightId)
        {
            return await _context.BookingFlights
                .Include(bf => bf.Booking)
                .Include(bf => bf.Flight)
                .Include(bf => bf.Tickets)
                .FirstOrDefaultAsync(bf => bf.BookingFlightId == bookingFlightId);
        }

        public async Task<IEnumerable<BookingFlight>> GetAllAsync()
        {
            return await _context.BookingFlights
                .OrderBy(bf => bf.BookingFlightId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingFlight>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.BookingFlights
                .Where(bf => bf.BookingId == bookingId)
                .Include(bf => bf.Flight)
                .OrderBy(bf => bf.BookingFlightId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingFlight>> GetByFlightIdAsync(int flightId)
        {
            return await _context.BookingFlights
                .Where(bf => bf.FlightId == flightId)
                .Include(bf => bf.Booking)
                .OrderBy(bf => bf.BookingFlightId)
                .ToListAsync();
        }

        public async Task<BookingFlight> CreateAsync(BookingFlight bookingFlight)
        {
            await _context.BookingFlights.AddAsync(bookingFlight);
            await _context.SaveChangesAsync();
            return bookingFlight;
        }

        public async Task<bool> UpdateAsync(BookingFlight bookingFlight)
        {
            _context.BookingFlights.Update(bookingFlight);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int bookingFlightId)
        {
            var item = await _context.BookingFlights.FindAsync(bookingFlightId);
            if (item == null) return false;

            _context.BookingFlights.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(int bookingId, int flightId)
        {
            return await _context.BookingFlights
                .AnyAsync(bf => bf.BookingId == bookingId && bf.FlightId == flightId);
        }
    }
}
