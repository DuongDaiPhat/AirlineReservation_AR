using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class PassengerService : IPassengerService
    {

        public async Task<Passenger?> GetByIdAsync(int passengerId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Passengers
                .FirstOrDefaultAsync(p => p.PassengerId == passengerId);
        }

        public async Task<Passenger?> GetByIdWithDetailsAsync(int passengerId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Passengers
                .Include(p => p.Booking)
                .Include(p => p.BookingServices)
                .Include(p => p.Tickets)
                .FirstOrDefaultAsync(p => p.PassengerId == passengerId);
        }

        public async Task<IEnumerable<Passenger>> GetAllAsync()
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Passengers
                .OrderBy(p => p.PassengerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Passenger>> GetByBookingIdAsync(int bookingId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Passengers
                .Where(p => p.BookingId == bookingId)
                .OrderBy(p => p.PassengerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Passenger>> GetByBookingWithDetailsAsync(int bookingId)
        {
            using var _context = DIContainer.CreateDb();
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
            using var _context = DIContainer.CreateDb();
            await _context.Passengers.AddAsync(passenger);
            await _context.SaveChangesAsync();
            return passenger;
        }

        public async Task<bool> UpdateAsync(Passenger passenger)
        {
            using var _context = DIContainer.CreateDb();
            _context.Passengers.Update(passenger);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int passengerId)
        {
            using var _context = DIContainer.CreateDb();
            var passenger = await _context.Passengers.FindAsync(passengerId);
            if (passenger == null) return false;

            _context.Passengers.Remove(passenger);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<int>> CreatePassengersAsync(int bookingId, List<PassengerDTO> passengers)
        {
            using var _db = DIContainer.CreateDb();
            var ids = new List<int>();

            foreach (var p in passengers)
            {
                var entity = new Passenger
                {
                    BookingId = bookingId,
                    PassengerType = p.PassengerType,

                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,

                    DateOfBirth = p.DateOfBirth,

                    IdNumber = p.PassportNumber,
                    Nationality = p.Nationality,
                    CountryIssue = p.CountryIssue,
                    ExpireDatePassport = p.PassportExpire
                };

                _db.Passengers.Add(entity);
                await _db.SaveChangesAsync();

                ids.Add(entity.PassengerId);
            }

            return ids;
        }
    }
}
