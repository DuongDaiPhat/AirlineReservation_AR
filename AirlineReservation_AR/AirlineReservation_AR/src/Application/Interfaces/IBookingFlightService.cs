using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IBookingFlightService
    {
        Task<BookingFlight?> GetByIdAsync(int bookingFlightId);
        Task<BookingFlight?> GetByIdWithDetailsAsync(int bookingFlightId);

        Task<IEnumerable<BookingFlight>> GetAllAsync();
        Task<IEnumerable<BookingFlight>> GetByBookingIdAsync(int bookingId);
        Task<IEnumerable<BookingFlight>> GetByFlightIdAsync(int flightId);

        Task<BookingFlight> CreateAsync(BookingFlight bookingFlight);
        Task<bool> UpdateAsync(BookingFlight bookingFlight);
        Task<bool> DeleteAsync(int bookingFlightId);

        Task<bool> ExistsAsync(int bookingId, int flightId);
    }
}
