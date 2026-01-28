using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.Application.Services
{
    /// <summary>
    /// Centralized service for all dropdown/lookup data with caching
    /// </summary>
    public class LookupService : ILookupService
    {
        private readonly MemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
        
        public LookupService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 100
            });
        }
        
        #region Airlines
        
        public async Task<List<AirlineSelectDto>> GetAirlinesAsync(bool activeOnly = true)
        {
            var cacheKey = $"Airlines_{activeOnly}";
            
            if (_cache.TryGetValue(cacheKey, out List<AirlineSelectDto> cached))
                return cached;
                
            using var db = DIContainer.CreateDb();
            
            var query = db.Airlines.AsQueryable();
            if (activeOnly) query = query.Where(a => a.IsActive);
            
            var airlines = await query
                .OrderBy(a => a.AirlineName)
                .Select(a => new AirlineSelectDto
                {
                    AirlineId = a.AirlineId,
                    Code = a.IataCode,
                    DisplayName = $"{a.AirlineName} ({a.IataCode})"
                })
                .ToListAsync();
                
            _cache.Set(cacheKey, airlines, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration,
                Size = 1
            });
            
            return airlines;
        }
        
        #endregion
        
        #region Airports
        
        public async Task<List<AirportSelectDto>> GetAirportsAsync(bool activeOnly = true)
        {
            var cacheKey = $"Airports_{activeOnly}";
            
            if (_cache.TryGetValue(cacheKey, out List<AirportSelectDto> cached))
                return cached;
                
            using var db = DIContainer.CreateDb();
            
            var query = db.Airports
                .Include(a => a.City)
                .AsQueryable();
                
            if (activeOnly) query = query.Where(a => a.IsActive);
            
            var airports = await query
                .OrderBy(a => a.City.CityName)
                .Select(a => new AirportSelectDto
                {
                    AirportId = a.AirportId,
                    Code = a.IataCode,
                    CityName = a.City.CityName,
                    DisplayName = $"{a.City.CityName} ({a.IataCode})"
                })
                .ToListAsync();
                
            _cache.Set(cacheKey, airports, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration,
                Size = 1
            });
            
            return airports;
        }
        
        #endregion
        
        #region Routes
        
        public async Task<List<RouteSelectDto>> GetActiveRoutesAsync()
        {
            const string cacheKey = "ActiveRoutes";
            
            if (_cache.TryGetValue(cacheKey, out List<RouteSelectDto> cached))
                return cached;
                
            using var db = DIContainer.CreateDb();
            
            // Query DISTINCT routes from actual flights
            var routes = await db.Flights
                .Where(f => f.Status == "Available" && f.FlightDate >= DateTime.Today)
                .Include(f => f.DepartureAirport)
                    .ThenInclude(a => a.City)
                .Include(f => f.ArrivalAirport)
                    .ThenInclude(a => a.City)
                .Select(f => new {
                    f.DepartureAirportId,
                    f.ArrivalAirportId,
                    DepartureCode = f.DepartureAirport.IataCode,
                    ArrivalCode = f.ArrivalAirport.IataCode,
                    DepartureCityName = f.DepartureAirport.City.CityName,
                    ArrivalCityName = f.ArrivalAirport.City.CityName
                })
                .Distinct()
                .ToListAsync();
                
            var result = routes.Select(r => new RouteSelectDto
            {
                Code = $"{r.DepartureCode}-{r.ArrivalCode}",
                DisplayName = $"{r.DepartureCityName} → {r.ArrivalCityName}",
                DepartureAirportId = r.DepartureAirportId,
                ArrivalAirportId = r.ArrivalAirportId
            }).ToList();
            
            _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration,
                Size = 1
            });
            
            return result;
        }
        
        #endregion
        
        #region Seat Classes
        
        public async Task<List<SeatClassSelectDto>> GetSeatClassesAsync()
        {
            const string cacheKey = "SeatClasses";
            
            if (_cache.TryGetValue(cacheKey, out List<SeatClassSelectDto> cached))
                return cached;
                
            using var db = DIContainer.CreateDb();
            
            var seatClasses = await db.SeatClasses
                .OrderBy(sc => sc.PriceMultiplier)
                .Select(sc => new SeatClassSelectDto
                {
                    SeatClassId = sc.SeatClassId,
                    Code = sc.ClassName,
                    DisplayName = sc.DisplayName ?? sc.ClassName,
                    PriceMultiplier = sc.PriceMultiplier
                })
                .ToListAsync();
            
            _cache.Set(cacheKey, seatClasses, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration,
                Size = 1
            });
            
            return seatClasses;
        }
        
        #endregion
        
        #region Statuses
        
        public async Task<List<StatusSelectDto>> GetBookingStatusesAsync()
        {
            // Return enum values as DTOs with English labels
            return await Task.FromResult(new List<StatusSelectDto>
            {
                new StatusSelectDto { Code = "Confirmed", DisplayName = "Confirmed" },
                new StatusSelectDto { Code = "Pending", DisplayName = "Pending" },
                new StatusSelectDto { Code = "Cancelled", DisplayName = "Cancelled" }
            });
        }
        
        public async Task<List<StatusSelectDto>> GetPaymentStatusesAsync()
        {
            return await Task.FromResult(new List<StatusSelectDto>
            {
                new StatusSelectDto { Code = "Completed", DisplayName = "Completed" },
                new StatusSelectDto { Code = "Pending", DisplayName = "Pending" },
                new StatusSelectDto { Code = "Failed", DisplayName = "Failed" },
                new StatusSelectDto { Code = "Refunded", DisplayName = "Refunded" }
            });
        }
        
        public async Task<List<StatusSelectDto>> GetFlightStatusesAsync()
        {
            return await Task.FromResult(new List<StatusSelectDto>
            {
                new StatusSelectDto { Code = "Available", DisplayName = "Available" },
                new StatusSelectDto { Code = "Full", DisplayName = "Full" },
                new StatusSelectDto { Code = "Cancelled", DisplayName = "Cancelled" }
            });
        }
        
        #endregion
        
        #region Roles
        
        public async Task<List<RoleSelectDto>> GetRolesAsync()
        {
            const string cacheKey = "Roles";
            
            if (_cache.TryGetValue(cacheKey, out List<RoleSelectDto> cached))
                return cached;
                
            using var db = DIContainer.CreateDb();
            
            var roles = await db.Roles
                .OrderBy(r => r.RoleId)
                .Select(r => new RoleSelectDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    DisplayName = r.RoleName
                })
                .ToListAsync();
            
            _cache.Set(cacheKey, roles, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration,
                Size = 1
            });
            
            return roles;
        }
        
        #endregion
        
        #region Cache Management
        
        public void InvalidateCache(string key)
        {
            _cache.Remove(key);
        }
        
        public void ClearAllCache()
        {
            _cache.Dispose();
        }
        
        #endregion
    }
}
