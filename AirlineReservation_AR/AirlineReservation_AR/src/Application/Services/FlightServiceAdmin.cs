using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class FlightServiceAdmin : IFlightServiceAdmin
    {
        private static List<FlightListDtoAdmin> _cachedFlights;
        private static DateTime _lastCacheTime;
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);
        private static readonly object _cacheLock = new object();
        public async Task<IEnumerable<FlightListDtoAdmin>> GetAllFlightsAsync()
        {
            using var db = DIContainer.CreateDb();

            var flights = await db.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft).ThenInclude(a => a.Seats)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.FlightPricings)
                .Include(f => f.BookingFlights).ThenInclude(bf => bf.Booking)
                .Select(f => new FlightListDtoAdmin
                {
                    FlightId = f.FlightId,
                    FlightCode = f.FlightNumber,
                    Route = f.DepartureAirport.AirportName + " → " + f.ArrivalAirport.AirportName,
                    Airline = f.Airline.AirlineName,
                    Aircraft = f.Aircraft.AircraftName ?? "Unknown",
                    FlightDate = f.FlightDate,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    BasePrice = f.BasePrice,
                    TotalSeats = f.Aircraft.Seats.Count(),
                    BookedSeats = f.BookingFlights.Count(bf => bf.Booking.Status == "CONFIRMED"),
                    Status = f.Status ?? "Scheduled"
                })
                .ToListAsync();

            return flights;
        }
        public async Task<PagedResult<FlightListDtoAdmin>> GetPagedFlightsAsync(int pageNumber, int pageSize)
        {
            // Validate parameters
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            // Kiểm tra và refresh cache nếu cần
            await EnsureCacheLoadedAsync();

            // Tính toán phân trang từ cache
            var totalRecords = _cachedFlights.Count;
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            // Đảm bảo pageNumber không vượt quá totalPages
            if (pageNumber > totalPages && totalPages > 0)
            {
                pageNumber = totalPages;
            }

            // Lấy dữ liệu của trang hiện tại
            var pagedData = _cachedFlights
                .OrderByDescending(f => f.FlightDate) // Sắp xếp theo ngày bay mới nhất
                .ThenBy(f => f.DepartureTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<FlightListDtoAdmin>
            {
                Items = pagedData,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<PagedResult<FlightListDtoAdmin>> SearchFlightsAsync(
            string searchTerm,
            int pageNumber,
            int pageSize)
        {
            // Validate parameters
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            // Đảm bảo cache đã load
            await EnsureCacheLoadedAsync();

            // Lọc dữ liệu theo search term
            var filteredFlights = _cachedFlights.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();
                filteredFlights = _cachedFlights
                    .Where(f =>
                        f.FlightCode.ToLower().Contains(searchTerm) ||
                        f.Route.ToLower().Contains(searchTerm) ||
                        f.Airline.ToLower().Contains(searchTerm) ||
                        f.Aircraft.ToLower().Contains(searchTerm) ||
                        f.Status.ToLower().Contains(searchTerm))
                    .AsQueryable();
            }

            var filteredList = filteredFlights.ToList();

            // Tính toán phân trang
            var totalRecords = filteredList.Count;
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (pageNumber > totalPages && totalPages > 0)
            {
                pageNumber = totalPages;
            }

            // Lấy dữ liệu trang hiện tại
            var pagedData = filteredList
                .OrderByDescending(f => f.FlightDate)
                .ThenBy(f => f.DepartureTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<FlightListDtoAdmin>
            {
                Items = pagedData,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
        private async Task EnsureCacheLoadedAsync()
        {
            lock (_cacheLock)
            {
                // Kiểm tra cache có tồn tại và còn hiệu lực không
                if (_cachedFlights != null &&
                    DateTime.Now - _lastCacheTime < CacheExpiration)
                {
                    return; // Cache còn hiệu lực, không cần reload
                }
            }

            // Cache hết hạn hoặc chưa có, cần reload
            await RefreshCacheAsync();
        }
        public async Task RefreshCacheAsync()
        {
            var flights = await GetAllFlightsAsync();

            lock (_cacheLock)
            {
                _cachedFlights = flights.ToList();
                _lastCacheTime = DateTime.Now;
            }
        }
        public void ClearCache()
        {
            lock (_cacheLock)
            {
                _cachedFlights = null;
            }
        }
        public bool IsCacheLoaded()
        {
            lock (_cacheLock)
            {
                return _cachedFlights != null &&
                       DateTime.Now - _lastCacheTime < CacheExpiration;
            }
        }
        public CacheInfo GetCacheInfo()
        {
            lock (_cacheLock)
            {
                return new CacheInfo
                {
                    IsLoaded = _cachedFlights != null,
                    RecordCount = _cachedFlights?.Count ?? 0,
                    LastUpdateTime = _lastCacheTime,
                    ExpirationTime = _lastCacheTime + CacheExpiration,
                    IsExpired = _cachedFlights == null ||
                                DateTime.Now - _lastCacheTime >= CacheExpiration
                };
            }
        }
        public async Task<bool> UpdateFlightAsync(FlightListDtoAdmin flightDto)
        {
            try
            {
                using var db = DIContainer.CreateDb();

                // Tìm flight trong database
                var flight = await db.Flights.FindAsync(flightDto.FlightId);

                if (flight == null)
                {
                    throw new Exception($"Không tìm thấy chuyến bay với ID: {flightDto.FlightId}");
                }

                // Cập nhật các trường
                flight.FlightNumber = flightDto.FlightCode;
                flight.FlightDate = flightDto.FlightDate;
                flight.DepartureTime = flightDto.DepartureTime;
                flight.ArrivalTime = flightDto.ArrivalTime;
                flight.BasePrice = flightDto.BasePrice;
                flight.Status = flightDto.Status;

                // Lưu thay đổi
                await db.SaveChangesAsync();

                // Clear cache để load lại dữ liệu mới
                ClearCache();

                return true;
            }
            catch (Exception ex)
            {
                // Log error nếu cần
                throw new Exception($"Lỗi khi cập nhật chuyến bay: {ex.Message}", ex);
            }
        }

        public async Task<bool> CancelFlightAsync(int flightId)
        {
            try
            {
                using var db = DIContainer.CreateDb();

                var flight = await db.Flights.FindAsync(flightId);

                if (flight == null)
                {
                    throw new Exception($"Không tìm thấy chuyến bay với ID: {flightId}");
                }

                // Cập nhật trạng thái thành Cancelled
                flight.Status = "Cancelled";

                await db.SaveChangesAsync();

                // Clear cache
                ClearCache();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi hủy chuyến bay: {ex.Message}", ex);
            }
        }
        public async Task<FlightListDtoAdmin> GetFlightByIdAsync(int flightId)
        {
            using var db = DIContainer.CreateDb();

            var flight = await db.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Where(f => f.FlightId == flightId)
                .Select(f => new FlightListDtoAdmin
                {
                    FlightId = f.FlightId,
                    FlightCode = f.FlightNumber,
                    Route = f.DepartureAirport.AirportName + " → " + f.ArrivalAirport.AirportName,
                    Airline = f.Airline.AirlineName,
                    Aircraft = f.Aircraft.AircraftName ?? "Unknown",
                    FlightDate = f.FlightDate,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    BasePrice = f.BasePrice,
                    TotalSeats = f.Aircraft.Seats.Count(),
                    BookedSeats = f.BookingFlights.Count(bf => bf.Booking.Status == "CONFIRMED"),
                    Status = f.Status ?? "Scheduled"
                })
                .FirstOrDefaultAsync();

            return flight;
        }
    }
}
