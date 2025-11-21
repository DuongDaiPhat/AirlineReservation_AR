using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingService : IBookingService
    {

        public async Task<Booking> CreateBookingAsync(BookingRequest req)
        {
            using var _db = DIContainer.CreateDb();
            var booking = new Booking
            {
                UserId = req.UserId,
                BookingDate = DateTime.Now,
                BookingReference = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),

                ContactEmail = req.Contact.Email,
                ContactPhone = req.Contact.Phone
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            var bf = new BookingFlight
            {
                BookingId = booking.BookingId,
                FlightId = req.SelectedFlight.FlightId,
                TripType = req.TripType
            };

            _db.BookingFlights.Add(bf);
            await _db.SaveChangesAsync();

            return booking;
        }
    }
}
