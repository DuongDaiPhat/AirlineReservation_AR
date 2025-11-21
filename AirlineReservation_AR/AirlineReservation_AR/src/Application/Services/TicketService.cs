using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class TicketService : ITicketService
    {

        public async Task<Ticket> CreateTicketAsync(
            int bookingFlightId,
            int passengerId,
            int seatClassId,
            decimal price,
            decimal? taxes,
            decimal? fees)
        {
            using var _db = DIContainer.CreateDb();
            var bookingFlight = await _db.BookingFlights
                .FirstOrDefaultAsync(x => x.BookingFlightId == bookingFlightId);

            if (bookingFlight == null)
                throw new Exception("BookingFlight not found.");

            var passenger = await _db.Passengers
                .FirstOrDefaultAsync(x => x.PassengerId == passengerId);

            if (passenger == null)
                throw new Exception("Passenger not found.");

            var seatClass = await _db.SeatClasses
                .FirstOrDefaultAsync(x => x.SeatClassId == seatClassId);

            if (seatClass == null)
                throw new Exception("Seat class not found.");

            var ticketNumber = $"TKT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                BookingFlightId = bookingFlightId,
                PassengerId = passengerId,
                SeatClassId = seatClassId,
                TicketNumber = ticketNumber,
                Price = price,
                Taxes = taxes,
                Fees = fees,
                Status = "Pending",
                CheckedInAt = null
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();

            return ticket;
        }

        public async Task<Ticket?> GetTicketByIdAsync(Guid ticketId)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Tickets
                .Include(t => t.BookingFlight)
                .Include(t => t.Passenger)
                .Include(t => t.SeatClass)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByPassengerAsync(int passengerId)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Tickets
                .Where(t => t.PassengerId == passengerId)
                .Include(t => t.BookingFlight)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByBookingFlightAsync(int bookingFlightId)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Tickets
                .Where(t => t.BookingFlightId == bookingFlightId)
                .Include(t => t.Passenger)
                .ToListAsync();
        }

        public async Task<bool> CheckInAsync(Guid ticketId, string seatNumber)
        {
            using var _db = DIContainer.CreateDb();
            var ticket = await _db.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.SeatNumber = seatNumber;
            ticket.CheckedInAt = DateTime.UtcNow;
            ticket.Status = "CheckedIn";

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(Guid ticketId, string status)
        {
            using var _db = DIContainer.CreateDb();
            var ticket = await _db.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.Status = status;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateSeatAsync(Guid ticketId, string newSeatNumber)
        {
            using var _db = DIContainer.CreateDb();
            var ticket = await _db.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.SeatNumber = newSeatNumber;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task CreateTicketsAsync(int bookingId, int flightId, int seatClassId)
        {
            using var _db = DIContainer.CreateDb();
            var passengers = _db.Passengers.Where(p => p.BookingId == bookingId).ToList();
            var bf = _db.BookingFlights.First(b => b.BookingId == bookingId && b.FlightId == flightId);

            foreach (var p in passengers)
            {
                _db.Tickets.Add(new Ticket
                {
                    TicketId = Guid.NewGuid(),
                    BookingFlightId = bf.BookingFlightId,
                    PassengerId = p.PassengerId,
                    SeatClassId = seatClassId
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}
