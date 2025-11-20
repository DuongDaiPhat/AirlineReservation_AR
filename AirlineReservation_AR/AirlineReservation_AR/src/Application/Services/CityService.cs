using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class CityService : ICityService
    {

        public async Task<City> CreateAsync(string cityCode, string cityName, string countryCode, bool isActive = true)
        {
            using var _db = DIContainer.CreateDb();
            var exists = await _db.Cities.AnyAsync(c => c.CityCode == cityCode);
            if (exists) throw new System.Exception("City already exists.");

            var country = await _db.Countries.FirstOrDefaultAsync(x => x.CountryCode == countryCode);
            if (country == null) throw new System.Exception("Country not found.");

            var city = new City
            {
                CityCode = cityCode,
                CityName = cityName,
                CountryCode = countryCode,
                IsActive = isActive
            };

            _db.Cities.Add(city);
            await _db.SaveChangesAsync();
            return city;
        }

        public async Task<City?> GetByCodeAsync(string cityCode)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Cities
                .Include(c => c.Country)
                .Include(c => c.Airports)
                .FirstOrDefaultAsync(c => c.CityCode == cityCode);
        }

        public async Task<IEnumerable<City>> GetAllAsync(bool includeInactive = false)
        {
            using var _db = DIContainer.CreateDb();
            var query = _db.Cities.AsQueryable();

            if (!includeInactive)
                query = query.Where(c => c.IsActive);

            return await query
                .Include(c => c.Country)
                .OrderBy(c => c.CityName)
                .ToListAsync();
        }

        public async Task<IEnumerable<City>> GetByCountryAsync(string countryCode, bool onlyActive = true)
        {
            using var _db = DIContainer.CreateDb();
            var query = _db.Cities.Where(c => c.CountryCode == countryCode);

            if (onlyActive)
                query = query.Where(c => c.IsActive);

            return await query
                .Include(c => c.Country)
                .OrderBy(c => c.CityName)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(
            string cityCode,
            string? cityName = null,
            string? countryCode = null,
            bool? isActive = null)
        {
            using var _db = DIContainer.CreateDb();
            var city = await _db.Cities.FindAsync(cityCode);
            if (city == null) return false;

            if (cityName != null)
                city.CityName = cityName;

            if (countryCode != null)
            {
                var country = await _db.Countries.FirstOrDefaultAsync(c => c.CountryCode == countryCode);
                if (country == null) throw new System.Exception("Country not found.");

                city.CountryCode = countryCode;
            }

            if (isActive.HasValue)
                city.IsActive = isActive.Value;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string cityCode)
        {
            using var _db = DIContainer.CreateDb();
            var city = await _db.Cities.FindAsync(cityCode);
            if (city == null) return false;

            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
