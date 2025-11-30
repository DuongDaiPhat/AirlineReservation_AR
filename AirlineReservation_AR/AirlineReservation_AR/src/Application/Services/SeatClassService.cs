using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class SeatClassService : ISeatClassService
    {
        public async Task<SeatClass> CreateAsync(
            string className,
            string displayName,
            decimal priceMultiplier,
            int? baggageAllowanceKg = null,
            int? cabinBaggageAllowanceKg = null,
            string? description = null,
            string? features = null)
        {
            using var _db = DIContainer.CreateDb();
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
            using var _db = DIContainer.CreateDb();
            return await _db.SeatClasses
                .FirstOrDefaultAsync(sc => sc.SeatClassId == seatClassId);
        }

        public async Task<SeatClass?> GetByClassNameAsync(string className)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.SeatClasses
                .FirstOrDefaultAsync(sc => sc.ClassName == className);
        }

        public async Task<IEnumerable<SeatClass>> GetAllAsync()
        {
            using var _db = DIContainer.CreateDb();
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
            using var _db = DIContainer.CreateDb();
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
            using var _db = DIContainer.CreateDb();
            var seatClass = await _db.SeatClasses.FindAsync(seatClassId);
            if (seatClass == null) return false;

            _db.SeatClasses.Remove(seatClass); 

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<SeatClass>> GetSeatClass()
        {
            using var _db = DIContainer.CreateDb();
            return await _db.SeatClasses
                .OrderBy(sc => sc.PriceMultiplier)
                .ToListAsync();
        }

        public SeatAvailabilityDTO GetSeatAvailability(int flightId)
        {
            using var db = DIContainer.CreateDb();

            // 1. Lấy flight
            var flight = db.Flights.FirstOrDefault(f => f.FlightId == flightId);
            if (flight == null)
                return null;

            int aircraftId = flight.AircraftId;

            // 2. Lấy tổng số ghế theo từng SeatClass
            var totalSeats = db.AircraftSeatConfigs
                .Where(x => x.AircraftId == aircraftId)
                .Select(x => new
                {
                    x.SeatClassId,
                    x.SeatCount
                })
                .ToList();

            // 3. Lấy số vé đã bán
            var bookedSeats = db.Tickets
                .Where(t => t.BookingFlight.FlightId == flightId
                            && t.Status != "Cancelled"
                            && t.Status != "Refunded")
                .GroupBy(t => t.SeatClassId)
                .Select(g => new
                {
                    SeatClassID = g.Key,
                    Count = g.Count()
                })
                .ToList();

            // 4. Ghép lại để tính số ghế còn lại
            var result = new Dictionary<string, int>();

            foreach (var seat in totalSeats)
            {
                int booked = bookedSeats
                    .FirstOrDefault(b => b.SeatClassID == seat.SeatClassId)?.Count ?? 0;

                // Lấy DisplayName
                string className = db.SeatClasses
                    .Where(c => c.SeatClassId == seat.SeatClassId)
                    .Select(c => c.DisplayName)
                    .First();

                int left = seat.SeatCount - booked;

                result[className] = left;
            }

            return new SeatAvailabilityDTO
            {
                SeatsLeftByClass = result
            };
        }
    }
}
