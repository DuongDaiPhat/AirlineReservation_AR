using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class BookingController
    {
        private readonly IBookingService _bookingService;
        private readonly IPaymenService _paymentService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<IEnumerable<Booking>> GetAllAsync() => await _bookingService.GetAllWithDetailsAsync();
        public async Task<Booking?> GetByReferenceAsync(string reference) => await _bookingService.GetByReferenceAsync(reference);

        public async Task<bool> ConfirmBookingAsync(string bookingReference)
        {
            var booking = await _bookingService.GetByReferenceAsync(bookingReference);
            if (booking == null) return false;

            booking.Status = "Đã xác nhận";
            var payment = booking.Payments?.FirstOrDefault();
            if (payment != null)
            {
                payment.Status = "Đã thanh toán";
                payment.ProcessedAt = DateTime.Now;
            }

            return await _bookingService.UpdateAsync(booking);
        }

        public async Task<bool> CancelBookingAsync(string bookingReference)
        {
            var booking = await _bookingService.GetByReferenceAsync(bookingReference);
            if (booking == null) return false;

            booking.Status = "Đã hủy";
            var payment = booking.Payments?.FirstOrDefault();
            if (payment != null)
            {
                payment.Status = "Thất bại";
                payment.RefundedAmount = payment.Amount;
            }

            return await _bookingService.UpdateAsync(booking);
        }
    }
}
