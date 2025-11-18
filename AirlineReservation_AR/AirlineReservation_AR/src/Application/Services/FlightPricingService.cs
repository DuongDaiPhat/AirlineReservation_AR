using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class FlightPricingService : IFlightPricingService
    {
        private readonly AirlineReservationDbContext _db;

        public FlightPricingService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<FlightPricing> CreateAsync(int flightId, int seatClassId, decimal price)
        {
            var flight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
            if (flight == null) throw new System.Exception("Flight not found.");

            var seatClass = await _db.SeatClasses.FirstOrDefaultAsync(s => s.SeatClassId == seatClassId);
            if (seatClass == null) throw new System.Exception("Seat class not found.");

            var exists = await _db.FlightPricings.AnyAsync(fp =>
                fp.FlightId == flightId && fp.SeatClassId == seatClassId);

            if (exists) throw new System.Exception("Pricing for this flight and seat class already exists.");

            var pricing = new FlightPricing
            {
                FlightId = flightId,
                SeatClassId = seatClassId,
                Price = price,
                BookedSeats = 0
            };

            _db.FlightPricings.Add(pricing);
            await _db.SaveChangesAsync();

            return pricing;
        }

        public async Task<FlightPricing?> GetByIdAsync(int pricingId)
        {
            return await _db.FlightPricings
                .Include(fp => fp.Flight)
                .Include(fp => fp.SeatClass)
                .FirstOrDefaultAsync(fp => fp.PricingId == pricingId);
        }

        public async Task<IEnumerable<FlightPricing>> GetByFlightAsync(int flightId)
        {
            return await _db.FlightPricings
                .Where(fp => fp.FlightId == flightId)
                .Include(fp => fp.SeatClass)
                .ToListAsync();
        }

        public async Task<IEnumerable<FlightPricing>> GetBySeatClassAsync(int seatClassId)
        {
            return await _db.FlightPricings
                .Where(fp => fp.SeatClassId == seatClassId)
                .Include(fp => fp.Flight)
                .ToListAsync();
        }

        public async Task<IEnumerable<FlightPricing>> GetAllAsync()
        {
            return await _db.FlightPricings
                .Include(fp => fp.Flight)
                .Include(fp => fp.SeatClass)
                .OrderBy(fp => fp.FlightId)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int pricingId, decimal? price = null, int? bookedSeats = null)
        {
            var pricing = await _db.FlightPricings.FindAsync(pricingId);
            if (pricing == null) return false;

            if (price.HasValue)
                pricing.Price = price.Value;

            if (bookedSeats.HasValue)
                pricing.BookedSeats = bookedSeats.Value;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int pricingId)
        {
            var pricing = await _db.FlightPricings.FindAsync(pricingId);
            if (pricing == null) return false;

            _db.FlightPricings.Remove(pricing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
