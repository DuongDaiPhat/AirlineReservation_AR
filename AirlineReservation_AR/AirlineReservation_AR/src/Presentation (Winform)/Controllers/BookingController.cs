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
        private readonly IPassengerService _passengerService;
        private readonly ITicketService _ticketService;
        private readonly IServiceService _baggageService; //bagageservice

        public BookingController(
        IBookingService bookingService,
        IPassengerService passengerService,
        ITicketService ticketService,
        IServiceService baggageService)
        {
            _bookingService = bookingService;
            _passengerService = passengerService;
            _ticketService = ticketService;
            _baggageService = baggageService;
        }

        public async Task<int> CreateFullBookingAsync(BookingRequest req)
        {
            var booking = await _bookingService.CreateBookingAsync(req);

            await _passengerService.CreatePassengersAsync(booking.BookingId, req.Passengers);

            await _ticketService.CreateTicketsAsync(
                booking.BookingId,
                req.SelectedFlight.FlightId,
                req.SelectedFlight.SeatClassId
            );

            await _baggageService.AddBaggageAsync(booking.BookingId, req.Baggage);

            return booking.BookingId;
        }
    }
}
