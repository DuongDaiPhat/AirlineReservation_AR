using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IBookingServiceService
    {
        Task<BookingService?> GetByIdAsync(int bookingServiceId);
        Task<BookingService?> GetByIdWithDetailsAsync(int bookingServiceId);

        Task<IEnumerable<BookingService>> GetAllAsync();
        Task<IEnumerable<BookingService>> GetByBookingIdAsync(int bookingId);
        Task<IEnumerable<BookingService>> GetByPassengerIdAsync(int passengerId);

        Task<BookingService> CreateAsync(BookingService bookingService);
        Task<bool> UpdateAsync(BookingService bookingService);
        Task<bool> DeleteAsync(int bookingServiceId);

        Task<decimal> GetTotalAmountByBookingAsync(int bookingId);
    }
}
