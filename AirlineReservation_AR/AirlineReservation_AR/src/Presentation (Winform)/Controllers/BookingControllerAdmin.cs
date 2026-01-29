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
    public class BookingControllerAdmin
    {
        private readonly IBookingServiceAdmin _bookingService;
        public BookingControllerAdmin(IBookingServiceAdmin bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<IEnumerable<BookingDtoAdmin>> GetAllAsync() => await _bookingService.GetAllBookingsAsync();

        public async Task<BookingDtoAdmin?> GetByReferenceAsync(string bookingReference)
        {
            return await _bookingService.GetBookingByReferenceAsync(bookingReference);
        }

        public async Task<IEnumerable<BookingDtoAdmin>> FilterAsync(BookingFilterDto filter)
        {
            return await _bookingService.FilterBookingsAsync(filter);
        }

        public async Task<bool> ConfirmBookingAsync(string bookingReference)
        {
            return await _bookingService.ConfirmPaymentAsync(bookingReference);
        }

        public async Task<bool> CancelBookingAsync(string bookingReference)
        {
            return await _bookingService.CancelBookingAsync(bookingReference);
        }

        public async Task<bool> UpdateCustomerAsync(string bookingRef, string name, string phone, string email)
        {
            return await _bookingService.UpdateBookingCustomerAsync(bookingRef, name, phone, email);
        }
    }
}
