using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface IFlightPricingService
    {
        Task<FlightPricing> CreateAsync(int flightId, int seatClassId, decimal price);
        Task<FlightPricing?> GetByIdAsync(int pricingId);
        Task<IEnumerable<FlightPricing>> GetByFlightAsync(int flightId);
        Task<IEnumerable<FlightPricing>> GetBySeatClassAsync(int seatClassId);
        Task<IEnumerable<FlightPricing>> GetAllAsync();

        Task<bool> UpdateAsync(
            int pricingId,
            decimal? price = null,
            int? bookedSeats = null);

        Task<bool> DeleteAsync(int pricingId);
    }
}
