using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    /// <summary>
    /// Service for fetching lookup/dropdown data with caching
    /// </summary>
    public interface ILookupService
    {
        // Airlines
        Task<List<AirlineSelectDto>> GetAirlinesAsync(bool activeOnly = true);
        
        // Airports
        Task<List<AirportSelectDto>> GetAirportsAsync(bool activeOnly = true);
        
        // Routes (derived from active flights)
        Task<List<RouteSelectDto>> GetActiveRoutesAsync();
        
        // Seat Classes
        Task<List<SeatClassSelectDto>> GetSeatClassesAsync();
        
        // Statuses (enum-based)
        Task<List<StatusSelectDto>> GetBookingStatusesAsync();
        Task<List<StatusSelectDto>> GetPaymentStatusesAsync();
        Task<List<StatusSelectDto>> GetFlightStatusesAsync();
        
        // Roles
        Task<List<RoleSelectDto>> GetRolesAsync();
        
        // Cache management
        void InvalidateCache(string key);
        void ClearAllCache();
    }
}
