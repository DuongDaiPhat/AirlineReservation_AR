using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly AirlineReservationDbContext _db;

        public AirlineService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<Airline> CreateAsync(
            string airlineName,
            string iataCode,
            string countryCode,
            string? contactEmail = null,
            string? contactPhone = null,
            string? website = null,
            string? logoUrl = null)
        {
            var duplicate = await _db.Airlines.AnyAsync(a => a.IataCode == iataCode);
            if (duplicate) throw new System.Exception("Airline with this IATA code already exists.");

            var country = await _db.Countries.FirstOrDefaultAsync(c => c.CountryCode == countryCode);
            if (country == null) throw new System.Exception("Country not found.");

            var airline = new Airline
            {
                AirlineName = airlineName,
                IataCode = iataCode,
                CountryCode = countryCode,
                ContactEmail = contactEmail,
                ContactPhone = contactPhone,
                Website = website,
                LogoUrl = logoUrl,
                IsActive = true
            };

            _db.Airlines.Add(airline);
            await _db.SaveChangesAsync();
            return airline;
        }

        public async Task<Airline?> GetByIdAsync(int airlineId)
        {
            return await _db.Airlines
                .Include(a => a.Country)
                .Include(a => a.Aircraft)
                .Include(a => a.Flights)
                .FirstOrDefaultAsync(a => a.AirlineId == airlineId);
        }

        public async Task<Airline?> GetByIataCodeAsync(string iataCode)
        {
            return await _db.Airlines
                .Include(a => a.Country)
                .FirstOrDefaultAsync(a => a.IataCode == iataCode);
        }

        public async Task<IEnumerable<Airline>> GetAllAsync(bool includeInactive = false)
        {
            var query = _db.Airlines.AsQueryable();

            if (!includeInactive)
                query = query.Where(a => a.IsActive);

            return await query
                .Include(a => a.Country)
                .OrderBy(a => a.AirlineName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Airline>> GetByCountryAsync(string countryCode, bool onlyActive = true)
        {
            var query = _db.Airlines.Where(a => a.CountryCode == countryCode);

            if (onlyActive)
                query = query.Where(a => a.IsActive);

            return await query
                .Include(a => a.Country)
                .OrderBy(a => a.AirlineName)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(
            int airlineId,
            string? airlineName = null,
            string? contactEmail = null,
            string? contactPhone = null,
            string? website = null,
            string? logoUrl = null,
            bool? isActive = null)
        {
            var airline = await _db.Airlines.FindAsync(airlineId);
            if (airline == null) return false;

            if (airlineName != null) airline.AirlineName = airlineName;
            if (contactEmail != null) airline.ContactEmail = contactEmail;
            if (contactPhone != null) airline.ContactPhone = contactPhone;
            if (website != null) airline.Website = website;
            if (logoUrl != null) airline.LogoUrl = logoUrl;
            if (isActive.HasValue) airline.IsActive = isActive.Value;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int airlineId)
        {
            var airline = await _db.Airlines.FindAsync(airlineId);
            if (airline == null) return false;

            _db.Airlines.Remove(airline);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
