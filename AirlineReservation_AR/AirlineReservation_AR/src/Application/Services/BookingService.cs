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

        public int CreateBooking(BookingCreateDTO dto)
        {
            using var db = DIContainer.CreateDb();

            var booking = new Booking
            {
                UserId = dto.UserId,
                BookingReference = "BK-" + DateTime.Now.Ticks,
                BookingDate = DateTime.Now,
                Status = "Pending",
                Currency = "VND",
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                SpecialRequests = dto.SpecialRequest
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            db.BookingFlights.Add(new BookingFlight
            {
                BookingId = booking.BookingId,
                FlightId = dto.FlightId,
                TripType = dto.TripType
            });

            foreach (var p in dto.Passengers)
            {
                p.BookingId = booking.BookingId;
                db.Passengers.Add(p);
            }

            db.SaveChanges();
            return booking.BookingId;
        }
    }
}
