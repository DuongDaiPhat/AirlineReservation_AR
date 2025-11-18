using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IAircraftTypeService
    {
        Task<AircraftType?> GetByIdAsync(int aircraftTypeId);
        Task<IEnumerable<AircraftType>> GetAllAsync();

        Task<AircraftType> CreateAsync(AircraftType type);
        Task<bool> UpdateAsync(AircraftType type);
        Task<bool> DeleteAsync(int aircraftTypeId);
    }
}
