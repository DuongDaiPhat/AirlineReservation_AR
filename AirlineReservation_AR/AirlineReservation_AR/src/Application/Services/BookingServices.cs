using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.Exceptions;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Shared.Helper;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingServices : IBookingService
    {

        public int CreateBooking(BookingCreateDTO dto, BookingCreateDTO? reDto)
        {
            using var db = DIContainer.CreateDb();

            var strategy = db.Database.CreateExecutionStrategy();
            return strategy.Execute(() =>
            {
                using var transaction = db.Database.BeginTransaction();

                try
                {
                    if (dto == null)
                        throw new BusinessException("Outbound booking data is null");

                    bool isRoundTrip = dto.TripType == "RoundTrip";

                    if (isRoundTrip && reDto == null)
                        throw new BusinessException("Return booking data is required for round trip");

                    // 1. Booking
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
                        Price = dto.TotalAmount + (isRoundTrip ? reDto!.TotalAmount : 0),
                        Taxes = dto.TaxAmount + (isRoundTrip ? reDto!.TaxAmount : 0),
                        Fees = dto.TotalFee + (isRoundTrip ? reDto!.TotalFee : 0)
                    };

                    db.Bookings.Add(booking);
                    db.SaveChanges(); // PHẢI có để lấy BookingId

                    // 2. Outbound BookingFlight (luôn có)
                    var outboundBF = new BookingFlight
                    {
                        BookingId = booking.BookingId,
                        FlightId = dto.FlightId
                    };

                    db.BookingFlights.Add(outboundBF);
                    db.SaveChanges(); // lấy BookingFlightId

                    // 3. Return BookingFlight (nếu round trip)
                    BookingFlight? returnBF = null;

                    if (isRoundTrip)
                    {
                        returnBF = new BookingFlight
                        {
                            BookingId = booking.BookingId,
                            FlightId = reDto!.FlightId
                        };

                        db.BookingFlights.Add(returnBF);
                        db.SaveChanges();
                    }

                    // 4. Passengers
                    foreach (var p in dto.Passengers)
                    {
                        p.Passenger.BookingId = booking.BookingId;
                        db.Passengers.Add(p.Passenger);
                    }
                    db.SaveChanges();

                    // 5. Services - OUTBOUND
                    AddServices(
                        db,
                        booking.BookingId,
                        outboundBF.BookingFlightId,
                        dto.Passengers
                    );

                    // 6. Services - RETURN (chỉ khi có)
                    if (isRoundTrip && returnBF != null)
                    {
                        AddServices(
                            db,
                            booking.BookingId,
                            returnBF.BookingFlightId,
                            reDto!.Passengers
                        );
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    return booking.BookingId;
                }
                catch (BusinessException)
                {
                    transaction.Rollback();
                    throw;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BusinessException("Có lỗi xảy ra khi tạo booking", ex);
                }
            });
            
        }


        private void AddServices(AirlineReservationDbContext db, int bookingId,int bookingFlightId,List<PassengerWithServicesDTO> passengers)
        {
            foreach (var bundle in passengers)
            {
                int passengerId = bundle.Passenger.PassengerId;
                var s = bundle.SelectedServices;
                if (s == null) continue;

                if (s.Baggage?.ServiceId > 0)
                {
                    db.BookingServices.Add(new BookingService
                    {
                        BookingId = bookingId,              
                        BookingFlightId = bookingFlightId,
                        PassengerId = passengerId,
                        ServiceId = s.Baggage.ServiceId,
                        Quantity = 1,
                        UnitPrice = s.Baggage.BasePrice
                    });
                }

                if (s.Meal?.ServiceId > 0)
                {
                    db.BookingServices.Add(new BookingService
                    {
                        BookingId = bookingId,
                        BookingFlightId = bookingFlightId,
                        PassengerId = passengerId,
                        ServiceId = s.Meal.ServiceId,
                        Quantity = 1,
                        UnitPrice = s.Meal.BasePrice
                    });
                }

                if (s.Priority?.ServiceId > 0)
                {
                    db.BookingServices.Add(new BookingService
                    {
                        BookingId = bookingId,
                        BookingFlightId = bookingFlightId,
                        PassengerId = passengerId,
                        ServiceId = s.Priority.ServiceId,
                        Quantity = 1,
                        UnitPrice = s.Priority.BasePrice
                    });
                }
            }
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
