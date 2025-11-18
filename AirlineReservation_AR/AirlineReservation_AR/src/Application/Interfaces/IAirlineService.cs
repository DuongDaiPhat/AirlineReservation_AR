using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface IAirlineService
    {
        Task<Airline> CreateAsync(
            string airlineName,
            string iataCode,
            string countryCode,
            string? contactEmail = null,
            string? contactPhone = null,
            string? website = null,
            string? logoUrl = null);

        Task<Airline?> GetByIdAsync(int airlineId);
        Task<Airline?> GetByIataCodeAsync(string iataCode);
        Task<IEnumerable<Airline>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<Airline>> GetByCountryAsync(string countryCode, bool onlyActive = true);

        Task<bool> UpdateAsync(
            int airlineId,
            string? airlineName = null,
            string? contactEmail = null,
            string? contactPhone = null,
            string? website = null,
            string? logoUrl = null,
            bool? isActive = null);

        Task<bool> DeleteAsync(int airlineId);
    }
}
