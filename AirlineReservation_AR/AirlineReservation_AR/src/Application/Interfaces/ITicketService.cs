using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(
            int bookingFlightId,
            int passengerId,
            int seatClassId,
            decimal price,
            decimal? taxes,
            decimal? fees);

        Task<Ticket?> GetTicketByIdAsync(Guid ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByPassengerAsync(int passengerId);
        Task<IEnumerable<Ticket>> GetTicketsByBookingFlightAsync(int bookingFlightId);

        Task<bool> CheckInAsync(Guid ticketId, string seatNumber);
        Task<bool> UpdateStatusAsync(Guid ticketId, string status);
        Task<bool> UpdateSeatAsync(Guid ticketId, string newSeatNumber);
    }
}
