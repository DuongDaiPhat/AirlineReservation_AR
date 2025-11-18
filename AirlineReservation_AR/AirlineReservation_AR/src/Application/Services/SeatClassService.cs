using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class SeatClassService : ISeatClassService
    {
        private readonly AirlineReservationDbContext _db;

        public SeatClassService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<SeatClass> CreateAsync(
            string className,
            string displayName,
            decimal priceMultiplier,
            int? baggageAllowanceKg = null,
            int? cabinBaggageAllowanceKg = null,
            string? description = null,
            string? features = null)
        {
            var exists = await _db.SeatClasses
                .AnyAsync(sc => sc.ClassName == className);

            if (exists)
                throw new Exception("Seat class with the same ClassName already exists.");

            var seatClass = new SeatClass
            {
                ClassName = className,
                DisplayName = displayName,
                PriceMultiplier = priceMultiplier,
                BaggageAllowanceKg = baggageAllowanceKg,
                CabinBaggageAllowanceKg = cabinBaggageAllowanceKg,
                Description = description,
                Features = features
            };

            _db.SeatClasses.Add(seatClass);
            await _db.SaveChangesAsync();

            return seatClass;
        }

        public async Task<SeatClass?> GetByIdAsync(int seatClassId)
        {
            return await _db.SeatClasses
                .FirstOrDefaultAsync(sc => sc.SeatClassId == seatClassId);
        }

        public async Task<SeatClass?> GetByClassNameAsync(string className)
        {
            return await _db.SeatClasses
                .FirstOrDefaultAsync(sc => sc.ClassName == className);
        }

        public async Task<IEnumerable<SeatClass>> GetAllAsync()
        {
            return await _db.SeatClasses
                .OrderBy(sc => sc.PriceMultiplier)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(
            int seatClassId,
            string? className = null,
            string? displayName = null,
            decimal? priceMultiplier = null,
            int? baggageAllowanceKg = null,
            int? cabinBaggageAllowanceKg = null,
            string? description = null,
            string? features = null)
        {
            var seatClass = await _db.SeatClasses.FindAsync(seatClassId);
            if (seatClass == null) return false;

            if (className != null)
            {
                var exists = await _db.SeatClasses
                    .AnyAsync(sc => sc.ClassName == className && sc.SeatClassId != seatClassId);

                if (exists)
                    throw new Exception("Another seat class with the same ClassName already exists.");

                seatClass.ClassName = className;
            }

            if (displayName != null)
                seatClass.DisplayName = displayName;

            if (priceMultiplier.HasValue)
                seatClass.PriceMultiplier = priceMultiplier.Value;

            if (baggageAllowanceKg.HasValue)
                seatClass.BaggageAllowanceKg = baggageAllowanceKg;

            if (cabinBaggageAllowanceKg.HasValue)
                seatClass.CabinBaggageAllowanceKg = cabinBaggageAllowanceKg;

            if (description != null)
                seatClass.Description = description;

            if (features != null)
                seatClass.Features = features;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int seatClassId)
        {
            var seatClass = await _db.SeatClasses.FindAsync(seatClassId);
            if (seatClass == null) return false;

            _db.SeatClasses.Remove(seatClass); 

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
