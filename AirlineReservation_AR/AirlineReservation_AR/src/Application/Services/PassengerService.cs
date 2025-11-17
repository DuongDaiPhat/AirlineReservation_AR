using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly AirlineReservationDbContext _context;

        public PassengerService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Passenger?> GetByIdAsync(int passengerId)
        {
            return await _context.Passengers
                .FirstOrDefaultAsync(p => p.PassengerId == passengerId);
        }

        public async Task<Passenger?> GetByIdWithDetailsAsync(int passengerId)
        {
            return await _context.Passengers
                .Include(p => p.Booking)
                .Include(p => p.BookingServices)
                .Include(p => p.Tickets)
                .FirstOrDefaultAsync(p => p.PassengerId == passengerId);
        }

        public async Task<IEnumerable<Passenger>> GetAllAsync()
        {
            return await _context.Passengers
                .OrderBy(p => p.PassengerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Passenger>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Passengers
                .Where(p => p.BookingId == bookingId)
                .OrderBy(p => p.PassengerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Passenger>> GetByBookingWithDetailsAsync(int bookingId)
        {
            return await _context.Passengers
                .Where(p => p.BookingId == bookingId)
                .Include(p => p.Booking)
                .Include(p => p.BookingServices)
                .Include(p => p.Tickets)
                .OrderBy(p => p.PassengerId)
                .ToListAsync();
        }

        public async Task<Passenger> CreateAsync(Passenger passenger)
        {
            await _context.Passengers.AddAsync(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<bool> UpdateAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int passengerId)
        {
            var passenger = await _context.Passengers.FindAsync(passengerId);
            if (passenger == null) return false;

            _context.Passengers.Remove(passenger);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
