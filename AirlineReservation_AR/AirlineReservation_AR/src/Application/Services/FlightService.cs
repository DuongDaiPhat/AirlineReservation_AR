using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class FlightService : IFlightService
    {
        private readonly AirlineReservationDbContext _context;

        public FlightService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Flight?> GetByIdAsync(int flightId)
        {
            return await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightId == flightId);
        }

        public async Task<Flight?> GetByIdWithDetailsAsync(int flightId)
        {
            return await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.FlightPricings)
                .FirstOrDefaultAsync(f => f.FlightId == flightId);
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _context.Flights
                .OrderBy(f => f.FlightDate)
                .ThenBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByAirlineAsync(int airlineId)
        {
            return await _context.Flights
                .Where(f => f.AirlineId == airlineId)
                .OrderBy(f => f.FlightDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByAircraftAsync(int aircraftId)
        {
            return await _context.Flights
                .Where(f => f.AircraftId == aircraftId)
                .OrderBy(f => f.FlightDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByRouteAsync(int departureAirportId, int arrivalAirportId)
        {
            return await _context.Flights
                .Where(f =>
                    f.DepartureAirportId == departureAirportId &&
                    f.ArrivalAirportId == arrivalAirportId)
                .OrderBy(f => f.FlightDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByDateAsync(DateTime date)
        {
            return await _context.Flights
                .Where(f => f.FlightDate.Date == date.Date)
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<Flight> CreateAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> UpdateAsync(Flight flight)
        {
            _context.Flights.Update(flight);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
