using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class AirportService : IAirportService
    {
        private readonly AirlineReservationDbContext _context;

        public AirportService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Airport?> GetByIdAsync(int airportId)
        {
            return await _context.Airports
                .FirstOrDefaultAsync(a => a.AirportId == airportId);
        }

        public async Task<Airport?> GetByIdWithDetailsAsync(int airportId)
        {
            return await _context.Airports
                .Include(a => a.City)
                .Include(a => a.DepartingFlights)
                .Include(a => a.ArrivingFlights)
                .FirstOrDefaultAsync(a => a.AirportId == airportId);
        }

        public async Task<IEnumerable<Airport>> GetAllAsync()
        {
            return await _context.Airports
                .OrderBy(a => a.AirportName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetActiveAirportsAsync()
        {
            return await _context.Airports
                .Where(a => a.IsActive)
                .OrderBy(a => a.AirportName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetByCityAsync(string cityCode)
        {
            return await _context.Airports
                .Where(a => a.CityCode == cityCode)
                .OrderBy(a => a.AirportName)
                .ToListAsync();
        }

        public async Task<Airport?> GetByIataCodeAsync(string iataCode)
        {
            return await _context.Airports
                .FirstOrDefaultAsync(a => a.IataCode == iataCode);
        }

        public async Task<Airport> CreateAsync(Airport airport)
        {
            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
            return airport;
        }

        public async Task<bool> UpdateAsync(Airport airport)
        {
            _context.Airports.Update(airport);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int airportId)
        {
            var airport = await _context.Airports.FindAsync(airportId);
            if (airport == null) return false;

            _context.Airports.Remove(airport);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
