using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IBookingServiceAdmin
    {
        Task<IEnumerable<BookingDtoAdmin>> GetAllBookingsAsync();
        Task<BookingDtoAdmin?> GetBookingByReferenceAsync(string bookingReference);
        Task<IEnumerable<BookingDtoAdmin>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<BookingDtoAdmin>> FilterBookingsAsync(BookingFilterDto filter);
        Task<bool> ConfirmPaymentAsync(string bookingReference);
        Task<bool> CancelBookingAsync(string bookingReference);
        Task<bool> UpdateBookingCustomerAsync(string bookingReference, string fullName, string phone, string email);
    }
}
