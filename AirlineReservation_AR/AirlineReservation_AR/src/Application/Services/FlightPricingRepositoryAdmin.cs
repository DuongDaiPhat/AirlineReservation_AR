using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class FlightPricingRepositoryAdmin : IFlightPricingRepositoryAdmin
    {
        private readonly AirlineReservationDbContext _context;

        public FlightPricingRepositoryAdmin(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FlightPricing>> GetAllAsync()
        {
            return await _context.FlightPricings
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Airline)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.DepartureAirport)
                    .ThenInclude(a => a.City)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.ArrivalAirport)
                    .ThenInclude(a => a.City)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Aircraft)
                    .ThenInclude(a => a.SeatConfigurations)
                .Include(fp => fp.SeatClass)
                .ToListAsync();
        }

        public async Task<FlightPricing?> GetByIdAsync(int pricingId)
        {
            return await _context.FlightPricings
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Airline)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.DepartureAirport)
                    .ThenInclude(a => a.City)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.ArrivalAirport)
                    .ThenInclude(a => a.City)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Aircraft)
                    .ThenInclude(a => a.SeatConfigurations)
                .Include(fp => fp.SeatClass)
                .FirstOrDefaultAsync(fp => fp.PricingId == pricingId);
        }

        public async Task<IEnumerable<FlightPricing>> GetByFlightIdAsync(int flightId)
        {
            return await _context.FlightPricings
                .Include(fp => fp.SeatClass)
                .Where(fp => fp.FlightId == flightId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FlightPricing>> GetWithFiltersAsync(
            string? route,
            string? seatClass,
            int? discountPercent)
        {
            using var db = DIContainer.CreateDb();
            var query = db.FlightPricings
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Airline)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.DepartureAirport)
                    .ThenInclude(a => a.City)
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.ArrivalAirport)
                    .ThenInclude(a => a.City)
                .Include(fp => fp.SeatClass)
                .AsNoTracking()
                .AsQueryable();

            // Apply route filter
            if (!string.IsNullOrEmpty(route) && route != "Tất cả")
            {
                var routeParts = route.Split('-').Select(r => r.Trim()).ToArray();
                if (routeParts.Length == 2)
                {
                    query = query.Where(fp =>
                        fp.Flight.DepartureAirport.IataCode == routeParts[0] &&
                        fp.Flight.ArrivalAirport.IataCode == routeParts[1]);
                }
            }

            // Apply seat class filter
            if (!string.IsNullOrEmpty(seatClass) && seatClass != "Tất cả")
            {
                query = query.Where(fp => fp.SeatClass.ClassName == seatClass);
            }

            // Apply discount filter
            if (discountPercent.HasValue)
            {
                query = query.Where(fp =>
                    ((fp.Flight.BasePrice - fp.Price) / fp.Flight.BasePrice * 100) >= discountPercent.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<FlightPricing> AddAsync(FlightPricing pricing)
        {
            await _context.FlightPricings.AddAsync(pricing);
            return pricing;
        }

        public async Task UpdateAsync(FlightPricing pricing)
        {
            _context.FlightPricings.Update(pricing);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int pricingId)
        {
            var pricing = await _context.FlightPricings.FindAsync(pricingId);
            if (pricing != null)
            {
                _context.FlightPricings.Remove(pricing);
            }
        }

        public async Task<int> GetAvailableSeatsAsync(int flightId, int seatClassId)
        {
            var pricing = await _context.FlightPricings
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Aircraft)
                    .ThenInclude(a => a.SeatConfigurations)
                .FirstOrDefaultAsync(fp => fp.FlightId == flightId && fp.SeatClassId == seatClassId);

            int totalSeats = pricing.Flight.Aircraft.SeatConfigurations
                .Where(sc => sc.SeatClassId == seatClassId)
                .Sum(sc => sc.SeatCount);

            return totalSeats - pricing.BookedSeats;
        }
        public async Task<FlightPricing?> GetByFlightAndClassAsync(int flightId, int seatClassId)
        {
            return await _context.FlightPricings
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Aircraft)
                        .ThenInclude(a => a.SeatConfigurations)
                .Include(fp => fp.SeatClass)
                .FirstOrDefaultAsync(fp => fp.FlightId == flightId && fp.SeatClassId == seatClassId);
        }
        public async Task<IEnumerable<FlightPricing>> GetForSearchAsync(
            string? route,
            string? seatClass,
            int? minDiscountPercent)
        {
            // ✅ Query tối ưu - CHỈ lấy dữ liệu cần thiết
            var query = _context.FlightPricings
                .Include(fp => fp.Flight)
                    .ThenInclude(f => f.Airline)
                .Include(fp => fp.Flight.DepartureAirport)
                .Include(fp => fp.Flight.ArrivalAirport)
                .Include(fp => fp.SeatClass)
                .AsNoTracking()
                .AsQueryable();

            // Route filter - ✅ Sử dụng IataCode thay vì City
            if (!string.IsNullOrEmpty(route) && route != "Tất cả")
            {
                var routeParts = route.Split(new[] { " - ", "-" }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(r => r.Trim())
                                      .ToArray();

                if (routeParts.Length == 2)
                {
                    query = query.Where(fp =>
                        fp.Flight.DepartureAirport.IataCode == routeParts[0] &&
                        fp.Flight.ArrivalAirport.IataCode == routeParts[1]);
                }
            }

            // Seat class filter
            if (!string.IsNullOrEmpty(seatClass) && seatClass != "Tất cả")
            {
                query = query.Where(fp => fp.SeatClass.ClassName == seatClass);
            }

            // Discount filter - ✅ Tính toán đúng
            if (minDiscountPercent.HasValue)
            {
                query = query.Where(fp =>
                    fp.Flight.BasePrice > 0 && // Tránh chia cho 0
                    ((fp.Flight.BasePrice - fp.Price) * 100 / fp.Flight.BasePrice) >= minDiscountPercent.Value);
            }

            return await query
                .OrderByDescending(fp => fp.Flight.DepartureTime) // Sắp xếp mới nhất
                .Take(100) // ✅ GIỚI HẠN 100 kết quả
                .ToListAsync();
        }
    }
}