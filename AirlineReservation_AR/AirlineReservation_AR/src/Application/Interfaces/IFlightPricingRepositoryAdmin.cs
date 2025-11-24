using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IFlightPricingRepositoryAdmin
    {
        Task<IEnumerable<FlightPricing>> GetAllAsync();
        Task<FlightPricing?> GetByIdAsync(int pricingId);
        Task<IEnumerable<FlightPricing>> GetByFlightIdAsync(int flightId);
        Task<IEnumerable<FlightPricing>> GetWithFiltersAsync(string? route, string? seatClass, int? discountPercent);
        Task<FlightPricing> AddAsync(FlightPricing pricing);
        Task UpdateAsync(FlightPricing pricing);
        Task DeleteAsync(int pricingId);
        Task<int> GetAvailableSeatsAsync(int flightId, int seatClassId);
        Task<FlightPricing?> GetByFlightAndClassAsync(int flightId, int seatClassId);
    }
}
