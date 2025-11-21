using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllWithDetailsAsync();
        Task<Booking?> GetByIdAsync(int bookingId);
        Task<Booking?> GetByReferenceAsync(string bookingReference);
        Task<bool> UpdateAsync(Booking booking);
        Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<IEnumerable<Booking>> SearchAsync(string keyword);
    }
}
