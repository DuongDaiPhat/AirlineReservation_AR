using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class FlightServiceAdmin : IFlightServiceAdmin
    {
        public async Task<IEnumerable<FlightListDtoAdmin>> GetAllFlightsAsync()
        {
            using var db = DIContainer.CreateDb();

            var flights = await db.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft).ThenInclude(a => a.Seats)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.FlightPricings)
                .Include(f => f.BookingFlights).ThenInclude(bf => bf.Booking)
                .Select(f => new FlightListDtoAdmin
                {
                    FlightId = f.FlightId,
                    FlightCode = f.FlightNumber,
                    Route = f.DepartureAirport.AirportName + " → " + f.ArrivalAirport.AirportName,
                    Airline = f.Airline.AirlineName,
                    Aircraft = f.Aircraft.AircraftName ?? "Unknown",
                    FlightDate = f.FlightDate,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    BasePrice = f.BasePrice,
                    TotalSeats = f.Aircraft.Seats.Count(),
                    BookedSeats = f.BookingFlights.Count(bf => bf.Booking.Status == "CONFIRMED"),
                    Status = f.Status ?? "Scheduled"
                })
                .ToListAsync();

            return flights;
        }
    }
}
