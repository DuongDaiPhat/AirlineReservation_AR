using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Shared.Helper;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingServices : IBookingService
    {

        public int CreateBooking(BookingCreateDTO dto)
        {
            using var db = DIContainer.CreateDb();

            var booking = new Booking
            {
                UserId = dto.UserId,
                BookingReference = BookingCodeGenerator.GenerateBookingCode(),
                BookingDate = DateTime.Now,
                Status = "Pending",
                Currency = "VND",
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                SpecialRequests = dto.SpecialRequest,
                Price = dto.TotalAmount,
                Taxes = dto.TaxAmount,
                Fees = dto.TotalFee
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            db.BookingFlights.Add(new BookingFlight
            {
                BookingId = booking.BookingId,
                FlightId = dto.FlightId,
                TripType = dto.TripType
            });
            db.SaveChanges();



            foreach (var p in dto.Passengers)
            {
                p.Passenger.BookingId = booking.BookingId;
                db.Passengers.Add(p.Passenger);
            }
            db.SaveChanges();

            foreach (var bundle in dto.Passengers)
            {
               int passengerId = bundle.Passenger.PassengerId;
               if(bundle.SelectedServices != null)
               {
                 if(bundle.SelectedServices.Baggage.ServiceId != 0)
                 {
                        db.BookingServices.Add(new BookingService
                        {
                            BookingId = booking.BookingId,
                            PassengerId = passengerId,
                            ServiceId = bundle.SelectedServices.Baggage.ServiceId,
                            Quantity = 1,
                            UnitPrice = bundle.SelectedServices.Baggage.BasePrice,
                        });
                 }

                 if (bundle.SelectedServices.Meal.ServiceId != 0)
                 {
                        db.BookingServices.Add(new BookingService
                        {
                            BookingId = booking.BookingId,
                            PassengerId = passengerId,
                            ServiceId = bundle.SelectedServices.Meal.ServiceId,
                            Quantity = 1,
                            UnitPrice = bundle.SelectedServices.Meal.BasePrice,
                        });
                 }

                 if (bundle.SelectedServices.Priority.ServiceId != 0)
                 {
                        db.BookingServices.Add(new BookingService
                        {
                            BookingId = booking.BookingId,
                            PassengerId = passengerId,
                            ServiceId = bundle.SelectedServices.Priority.ServiceId,
                            Quantity = 1,
                            UnitPrice = bundle.SelectedServices.Priority.BasePrice,
                        });
                  }

                }

            }
            db.SaveChanges();
            return booking.BookingId;
        }

        public async Task<IEnumerable<Booking>> GetAllWithDetailsAsync()
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Airline)
                .Include(b => b.Payments)
                .Include(b => b.BookingServices)
                    .ThenInclude(bs => bs.Service)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int bookingId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .Include(b => b.BookingServices)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<Booking?> GetByReferenceAsync(string bookingReference)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.Payments)
                .Include(b => b.BookingServices)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            using var _context = DIContainer.CreateDb();
            try
            {
                _context.Bookings.Update(booking);

                if (booking.Payments != null)
                    foreach (var p in booking.Payments)
                        _context.Payments.Update(p);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<Booking>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Bookings
                .Where(b => b.BookingDate >= from && b.BookingDate <= to)
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> SearchAsync(string keyword)
        {
            using var _context = DIContainer.CreateDb();
            if (string.IsNullOrWhiteSpace(keyword)) return await GetAllWithDetailsAsync();

            keyword = keyword.ToLower();
            return await _context.Bookings
                .Where(b =>
                    b.BookingReference.ToLower().Contains(keyword) ||
                    b.ContactEmail.ToLower().Contains(keyword) ||
                    b.ContactPhone.Contains(keyword))
                .Include(b => b.User)
                .Include(b => b.Passengers)
                .Include(b => b.BookingFlights).ThenInclude(bf => bf.Flight)
                .Include(b => b.Payments)
                .ToListAsync();
        }

        public Task<List<Booking>> GetBookingsByUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
