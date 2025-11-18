using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly AirlineReservationDbContext _db;

        public TicketService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<Ticket> CreateTicketAsync(
            int bookingFlightId,
            int passengerId,
            int seatClassId,
            decimal price,
            decimal? taxes,
            decimal? fees)
        {
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
            return await _db.Tickets
                .Include(t => t.BookingFlight)
                .Include(t => t.Passenger)
                .Include(t => t.SeatClass)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByPassengerAsync(int passengerId)
        {
            return await _db.Tickets
                .Where(t => t.PassengerId == passengerId)
                .Include(t => t.BookingFlight)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByBookingFlightAsync(int bookingFlightId)
        {
            return await _db.Tickets
                .Where(t => t.BookingFlightId == bookingFlightId)
                .Include(t => t.Passenger)
                .ToListAsync();
        }

        public async Task<bool> CheckInAsync(Guid ticketId, string seatNumber)
        {
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
            var ticket = await _db.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.Status = status;
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateSeatAsync(Guid ticketId, string newSeatNumber)
        {
            var ticket = await _db.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.SeatNumber = newSeatNumber;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
