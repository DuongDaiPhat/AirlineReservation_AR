using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface ICityService
    {
        Task<City> CreateAsync(string cityCode, string cityName, string countryCode, bool isActive = true);
        Task<City?> GetByCodeAsync(string cityCode);
        Task<IEnumerable<City>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<City>> GetByCountryAsync(string countryCode, bool onlyActive = true);

        Task<bool> UpdateAsync(
            string cityCode,
            string? cityName = null,
            string? countryCode = null,
            bool? isActive = null);

        Task<bool> DeleteAsync(string cityCode);
    }
}
