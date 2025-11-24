using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class BookingController
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<IEnumerable<Booking>> GetAllAsync() => await _bookingService.GetAllWithDetailsAsync();
        public async Task<Booking?> GetByReferenceAsync(string reference) => await _bookingService.GetByReferenceAsync(reference);

        public int CreateBooking(BookingCreateDTO dto)
            => _bookingService.CreateBooking(dto);
    }
}
