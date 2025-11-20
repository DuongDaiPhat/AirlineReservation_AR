using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IFlightService
    {
        Task<Flight?> GetByIdAsync(int flightId);
        Task<Flight?> GetByIdWithDetailsAsync(int flightId);

        Task<IEnumerable<Flight>> GetAllAsync();
        Task<IEnumerable<Flight>> GetByAirlineAsync(int airlineId);
        Task<IEnumerable<Flight>> GetByAircraftAsync(int aircraftId);
        Task<IEnumerable<Flight>> GetByRouteAsync(int departureAirportId, int arrivalAirportId);
        Task<IEnumerable<Flight>> GetByDateAsync(DateTime date);

        Task<Flight> CreateAsync(Flight flight);
        Task<bool> UpdateAsync(Flight flight);
        Task<bool> DeleteAsync(int flightId);

        Task<FlightSearchResultDTO> SearchAsync(FlightSearchParams p);
    }
}
