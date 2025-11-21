using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class BookingServiceService : IBookingServiceService
    {
        private readonly AirlineReservationDbContext _context;

        public BookingServiceService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingService?> GetByIdAsync(int bookingServiceId)
        {
            return await _context.BookingServices
                .FirstOrDefaultAsync(bs => bs.BookingServiceId == bookingServiceId);
        }

        public async Task<BookingService?> GetByIdWithDetailsAsync(int bookingServiceId)
        {
            return await _context.BookingServices
                .Include(bs => bs.Booking)
                .Include(bs => bs.Service)
                .Include(bs => bs.Passenger)
                .FirstOrDefaultAsync(bs => bs.BookingServiceId == bookingServiceId);
        }

        

        public async Task<IEnumerable<BookingService>> GetAllAsync()
        {
            return await _context.BookingServices
                .OrderBy(bs => bs.BookingServiceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingService>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.BookingServices
                .Where(bs => bs.BookingId == bookingId)
                .OrderBy(bs => bs.BookingServiceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingService>> GetByPassengerIdAsync(int passengerId)
        {
            return await _context.BookingServices
                .Where(bs => bs.PassengerId == passengerId)
                .OrderBy(bs => bs.BookingServiceId)
                .ToListAsync();
        }

        public async Task<BookingService> CreateAsync(BookingService bookingService)
        {
            await _context.BookingServices.AddAsync(bookingService);
            await _context.SaveChangesAsync();
            return bookingService;
        }

        public async Task<bool> UpdateAsync(BookingService bookingService)
        {
            _context.BookingServices.Update(bookingService);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int bookingServiceId)
        {
            var bookingService = await _context.BookingServices.FindAsync(bookingServiceId);
            if (bookingService == null) return false;

            _context.BookingServices.Remove(bookingService);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<decimal> GetTotalAmountByBookingAsync(int bookingId)
        {
            return await _context.BookingServices
                .Where(bs => bs.BookingId == bookingId)
                .Select(bs => bs.UnitPrice * bs.Quantity)
                .SumAsync();
        }
    }
}
