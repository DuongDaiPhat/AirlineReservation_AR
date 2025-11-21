using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IPassengerService
    {
        Task<Passenger?> GetByIdAsync(int passengerId);
        Task<Passenger?> GetByIdWithDetailsAsync(int passengerId);
        Task<IEnumerable<Passenger>> GetAllAsync();

        Task<IEnumerable<Passenger>> GetByBookingIdAsync(int bookingId);
        Task<IEnumerable<Passenger>> GetByBookingWithDetailsAsync(int bookingId);

        Task<Passenger> CreateAsync(Passenger passenger);
        Task<bool> UpdateAsync(Passenger passenger);
        Task<bool> DeleteAsync(int passengerId);

        Task<List<int>> CreatePassengersAsync(int bookingId, List<PassengerDTO> passengers);
    }
}
