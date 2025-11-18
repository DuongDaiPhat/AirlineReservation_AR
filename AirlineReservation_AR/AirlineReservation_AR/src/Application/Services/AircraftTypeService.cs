using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class AircraftTypeService : IAircraftTypeService
    {
        private readonly AirlineReservationDbContext _context;

        public AircraftTypeService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<AircraftType?> GetByIdAsync(int aircraftTypeId)
        {
            return await _context.AircraftTypes
                .FirstOrDefaultAsync(t => t.AircraftTypeId == aircraftTypeId);
        }

        public async Task<IEnumerable<AircraftType>> GetAllAsync()
        {
            return await _context.AircraftTypes
                .OrderBy(t => t.TypeName)
                .ToListAsync();
        }

        public async Task<AircraftType> CreateAsync(AircraftType type)
        {
            await _context.AircraftTypes.AddAsync(type);
            await _context.SaveChangesAsync();
            return type;
        }

        public async Task<bool> UpdateAsync(AircraftType type)
        {
            _context.AircraftTypes.Update(type);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int aircraftTypeId)
        {
            var item = await _context.AircraftTypes.FindAsync(aircraftTypeId);
            if (item == null) return false;

            _context.AircraftTypes.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
