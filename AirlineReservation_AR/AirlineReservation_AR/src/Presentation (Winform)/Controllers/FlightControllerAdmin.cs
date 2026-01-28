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
                throw new Exception($"Error retrieving flight details: {ex.Message}", ex);
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
        public async Task<bool> CreateFlightAsync(CreateFlightDtoAdmin flight)
        {
            try
            {
                return await _flightService.CreateFlightAsync(flight);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating flight: {ex.Message}", ex);
            }
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
                throw new Exception($"Error updating flight: {ex.Message}", ex);
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
                throw new Exception($"Error cancelling flight: {ex.Message}", ex);
            }
        }
        public async Task<bool> DeleteFlightAsync(int flightId)
        {
            try
            {
                return await _flightService.DeleteFlightAsync(flightId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting flight: {ex.Message}", ex);
            }
        }
        private void ValidateFlight(FlightListDtoAdmin flight)
        {
            if (flight == null)
                throw new ArgumentNullException(nameof(flight), "Flight information cannot be null");

            if (string.IsNullOrWhiteSpace(flight.FlightCode))
                throw new ArgumentException("Flight Number cannot be empty");

            if (flight.FlightDate < DateTime.Today)
                throw new ArgumentException("Flight Date cannot be in the past");

            if (flight.DepartureTime >= flight.ArrivalTime)
                throw new ArgumentException("Departure Time must be before Arrival Time");

            if (flight.BasePrice <= 0)
                throw new ArgumentException("Base Price must be greater than 0");

            if (flight.TotalSeats <= 0)
                throw new ArgumentException("Total Seats must be greater than 0");

            if (flight.AvailableSeats < 0 || flight.AvailableSeats > flight.TotalSeats)
                throw new ArgumentException("Available Seats are invalid");
        }
    }
}
