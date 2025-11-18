using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface IAircraftSeatConfigService
    {
        Task<AircraftSeatConfig> CreateAsync(
            int aircraftId,
            int seatClassId,
            int seatCount,
            int? rowStart = null,
            int? rowEnd = null);

        Task<AircraftSeatConfig?> GetByIdAsync(int configId);
        Task<IEnumerable<AircraftSeatConfig>> GetByAircraftAsync(int aircraftId);
        Task<IEnumerable<AircraftSeatConfig>> GetBySeatClassAsync(int seatClassId);

        Task<bool> UpdateAsync(
            int configId,
            int? seatCount = null,
            int? rowStart = null,
            int? rowEnd = null);

        Task<bool> DeleteAsync(int configId);
    }
}
