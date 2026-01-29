using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IFlightServiceAdmin
    {
        Task<IEnumerable<FlightListDtoAdmin>> GetAllFlightsAsync();
        Task<PagedResult<FlightListDtoAdmin>> GetPagedFlightsAsync(int pageNumber, int pageSize);
        Task<PagedResult<FlightListDtoAdmin>> SearchFlightsAsync(string searchTerm, int pageNumber, int pageSize);
        Task RefreshCacheAsync();
        void ClearCache();
        bool IsCacheLoaded();
        CacheInfo GetCacheInfo();

        Task<bool> CreateFlightAsync(CreateFlightDtoAdmin flight);
        Task<bool> UpdateFlightAsync(FlightListDtoAdmin flight);
        Task<bool> CancelFlightAsync(int flightId);
        Task<bool> DeleteFlightAsync(int flightId);
        Task<FlightListDtoAdmin> GetFlightByIdAsync(int flightId);
    }
}
