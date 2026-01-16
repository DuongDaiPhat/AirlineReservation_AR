using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class FlightControllerAdmin
    {
        private readonly IFlightServiceAdmin _flightService;

        public FlightControllerAdmin(IFlightServiceAdmin flightService)
        {
            _flightService = flightService;
        }

        public async Task<IEnumerable<FlightListDtoAdmin>> GetAllFlightsAsync()
        {
            return await _flightService.GetAllFlightsAsync();
        }
        public async Task<FlightListDtoAdmin> GetFlightByIdAsync(int flightId)
        {
            try
            {
                return await _flightService.GetFlightByIdAsync(flightId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin chuyến bay: {ex.Message}", ex);
            }
        }
        public async Task<PagedResult<FlightListDtoAdmin>> GetPagedFlightsAsync(int pageNumber, int pageSize)
        {
            return await _flightService.GetPagedFlightsAsync(pageNumber, pageSize);
        }
        public async Task<PagedResult<FlightListDtoAdmin>> SearchFlightsAsync(string searchTerm, int pageNumber, int pageSize)
        {
            return await _flightService.SearchFlightsAsync(searchTerm, pageNumber, pageSize);
        }
        public async Task RefreshCacheAsync()
        {
            await _flightService.RefreshCacheAsync();
        }
        public void ClearCache()
        {
            _flightService.ClearCache();
        }
        public bool IsCacheLoaded()
        {
            return _flightService.IsCacheLoaded();
        }
        public CacheInfo GetCacheInfo()
        {
            return _flightService.GetCacheInfo();
        }
        public async Task<bool> UpdateFlightAsync(FlightListDtoAdmin flight)
        {
            try
            {
                // Validate
                ValidateFlight(flight);

                return await _flightService.UpdateFlightAsync(flight);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chuyến bay: {ex.Message}", ex);
            }
        }
        public async Task<bool> CancelFlightAsync(int flightId)
        {
            try
            {
                return await _flightService.CancelFlightAsync(flightId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi hủy chuyến bay: {ex.Message}", ex);
            }
        }
        private void ValidateFlight(FlightListDtoAdmin flight)
        {
            if (flight == null)
                throw new ArgumentNullException(nameof(flight), "Thông tin chuyến bay không được null");

            if (string.IsNullOrWhiteSpace(flight.FlightCode))
                throw new ArgumentException("Số hiệu chuyến bay không được rỗng");

            if (flight.FlightDate < DateTime.Today)
                throw new ArgumentException("Ngày bay không được là ngày trong quá khứ");

            if (flight.DepartureTime >= flight.ArrivalTime)
                throw new ArgumentException("Giờ khởi hành phải trước giờ đến");

            if (flight.BasePrice <= 0)
                throw new ArgumentException("Giá vé phải lớn hơn 0");

            if (flight.TotalSeats <= 0)
                throw new ArgumentException("Số ghế phải lớn hơn 0");

            if (flight.AvailableSeats < 0 || flight.AvailableSeats > flight.TotalSeats)
                throw new ArgumentException("Số ghế còn trống không hợp lệ");
        }
    }
}
