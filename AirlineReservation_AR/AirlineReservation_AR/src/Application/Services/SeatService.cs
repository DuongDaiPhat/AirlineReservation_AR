using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class SeatService : ISeatService
    {
        private readonly AirlineReservationDbContext _context;

        public SeatService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return await _context.Seats
                .Include(s => s.Aircraft)
                .Include(s => s.SeatClass)
                .ToListAsync();
        }

        public async Task<Seat?> GetByIdAsync(int seatId)
        {
            return await _context.Seats
                .Include(s => s.Aircraft)
                .Include(s => s.SeatClass)
                .FirstOrDefaultAsync(s => s.SeatId == seatId);
        }

        public async Task<IEnumerable<Seat>> GetByAircraftAsync(int aircraftId)
        {
            return await _context.Seats
                .Where(s => s.AircraftId == aircraftId)
                .Include(s => s.SeatClass)
                .ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int aircraftId, int seatClassId)
        {
            return await _context.Seats
                .Where(s => s.AircraftId == aircraftId &&
                            s.SeatClassId == seatClassId &&
                            s.IsAvailable)
                .Include(s => s.SeatClass)
                .ToListAsync();
        }

        public async Task<Seat> CreateAsync(Seat seat)
        {
            await _context.Seats.AddAsync(seat);
            await _context.SaveChangesAsync();
            return seat;
        }

        public async Task<bool> UpdateAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int seatId)
        {
            var seat = await _context.Seats.FindAsync(seatId);
            if (seat == null) return false;

            _context.Seats.Remove(seat);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetSeatAvailabilityAsync(int seatId, bool isAvailable)
        {
            var seat = await _context.Seats.FindAsync(seatId);
            if (seat == null) return false;

            seat.IsAvailable = isAvailable;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
