using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly AirlineReservationDbContext _context;

        public CountryService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Country?> GetByCodeAsync(string countryCode)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.CountryCode == countryCode);
        }

        public async Task<Country?> GetByCodeWithDetailsAsync(string countryCode)
        {
            return await _context.Countries
                .Include(c => c.Cities)
                .Include(c => c.Airlines)
                .FirstOrDefaultAsync(c => c.CountryCode == countryCode);
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries
                .OrderBy(c => c.CountryName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetActiveCountriesAsync()
        {
            return await _context.Countries
                .Where(c => c.IsActive)
                .OrderBy(c => c.CountryName)
                .ToListAsync();
        }

        public async Task<Country> CreateAsync(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<bool> UpdateAsync(Country country)
        {
            _context.Countries.Update(country);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string countryCode)
        {
            var country = await _context.Countries.FindAsync(countryCode);
            if (country == null) return false;

            _context.Countries.Remove(country);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
