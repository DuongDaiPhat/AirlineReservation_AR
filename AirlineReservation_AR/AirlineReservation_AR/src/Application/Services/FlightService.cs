using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class FlightService : IFlightService
    {

        public async Task<Flight?> GetByIdAsync(int flightId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightId == flightId);
        }

        public async Task<Flight?> GetByIdWithDetailsAsync(int flightId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft)
                .Include(f => f.DepartureAirport).ThenInclude(a => a.City)
                .Include(f => f.ArrivalAirport).ThenInclude(a => a.City)
                .Include(f => f.FlightPricings)
                .FirstOrDefaultAsync(f => f.FlightId == flightId);
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .OrderBy(f => f.FlightDate)
                .ThenBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByAirlineAsync(int airlineId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .Where(f => f.AirlineId == airlineId)
                .OrderBy(f => f.FlightDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByAircraftAsync(int aircraftId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .Where(f => f.AircraftId == aircraftId)
                .OrderBy(f => f.FlightDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByRouteAsync(int departureAirportId, int arrivalAirportId)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .Where(f =>
                    f.DepartureAirportId == departureAirportId &&
                    f.ArrivalAirportId == arrivalAirportId)
                .OrderBy(f => f.FlightDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetByDateAsync(DateTime date)
        {
            using var _context = DIContainer.CreateDb();
            return await _context.Flights
                .Where(f => f.FlightDate.Date == date.Date)
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<Flight> CreateAsync(Flight flight)
        {
            using var _context = DIContainer.CreateDb();
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> UpdateAsync(Flight flight)
        {
            using var _context = DIContainer.CreateDb();
            _context.Flights.Update(flight);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int flightId)
        {
            using var _context = DIContainer.CreateDb();
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<FlightSearchResultDTO> SearchAsync(FlightSearchParams p)
        {
            using var db = DIContainer.CreateDb();

            var fromAirportId = await db.Airports
                .Where(a => a.IataCode == p.FromCode)
                .Select(a => a.AirportId)
                .FirstAsync();

            var toAirportId = await db.Airports
                .Where(a => a.IataCode == p.ToCode)
                .Select(a => a.AirportId)
                .FirstAsync();

            // DayTabs trước
            var dayTabs = new List<FlightDayPriceDTO>();
            for (int offset = -3; offset <= 3; offset++)
            {
                var date = p.StartDate!.Value.AddDays(offset);
                dayTabs.Add(new FlightDayPriceDTO { Date = date, LowestPrice = 0 });
            }

            // Lấy flight list
            var flights = await db.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft).ThenInclude(a => a.AircraftType)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.FlightPricings)
                .Where(f =>
                    f.DepartureAirportId == fromAirportId &&
                    f.ArrivalAirportId == toAirportId &&
                    f.FlightDate == p.StartDate)
                .ToListAsync();

            if (!flights.Any())
            {
                return new FlightSearchResultDTO
                {
                    AllFlights = new List<FlightResultDTO>(),
                    AirlineFilters = new List<AirlineFilterDTO>(),
                    DayTabs = dayTabs
                };
            }

            // Convert SeatClassId => DisplayName
            string selectedSeatClassName = await db.SeatClasses
                .Where(s => s.SeatClassId == p.SeatClassId)
                .Select(s => s.DisplayName)
                .FirstAsync();

            var seatClassMap = await db.SeatClasses
                .ToDictionaryAsync(s => s.SeatClassId, s => s.DisplayName);

            var resultList = new List<FlightResultDTO>();

            foreach (var f in flights)
            {
                var seatsLeft = await GetSeatAvailabilityAsync(f.FlightId);

                seatsLeft.TryGetValue(selectedSeatClassName, out int available);

                var priceRow = f.FlightPricings.FirstOrDefault(x => x.SeatClassId == p.SeatClassId);

                resultList.Add(new FlightResultDTO
                {
                    FlightId = f.FlightId,
                    AirlineName = f.Airline.AirlineName,
                    AirlineLogo = f.Airline.LogoUrl,

                    FromAirportCode = f.DepartureAirport.IataCode,
                    ToAirportCode = f.ArrivalAirport.IataCode,
                    FromAirportName = f.DepartureAirport.AirportName,
                    ToAirportName = f.ArrivalAirport.AirportName,

                    FlightDate = f.FlightDate,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    DurationMinutes = f.DurationMinutes ?? 0,

                    Price = priceRow?.Price ?? f.BasePrice,

                    AvailableSeats = available,
                    SeatsLeftByClass = seatsLeft,
                    SeatClassesMap = seatClassMap,
                    SelectedSeatClassName = selectedSeatClassName,

                    TotalSeatsLeft = seatsLeft.Values.Sum(),

                    AircraftType = f.Aircraft.AircraftType.DisplayName
                });
            }

            resultList = resultList.OrderBy(f => f.Price).ToList();

            var airlineFilters = resultList
                .GroupBy(f => f.AirlineName)
                .Select(g => new AirlineFilterDTO
                {
                    AirlineName = g.Key,
                    LogoUrl = g.First().AirlineLogo
                })
                .ToList();

            return new FlightSearchResultDTO
            {
                AllFlights = resultList,
                AirlineFilters = airlineFilters,
                DayTabs = dayTabs
            };
        }

        // DisplayName-based response
        public async Task<Dictionary<string, int>> GetSeatAvailabilityAsync(int flightId)
        {
            using var db = DIContainer.CreateDb();

            var flight = await db.Flights
                .AsNoTracking()
                .FirstAsync(f => f.FlightId == flightId);

            int aircraftId = flight.AircraftId;

            var totalSeats = await db.AircraftSeatConfigs
                .Where(c => c.AircraftId == aircraftId)
                .ToListAsync();

            var booked = await db.Tickets
                .Where(t => t.BookingFlight.FlightId == flightId &&
                            t.Status != "Cancelled" &&
                            t.Status != "Refunded")
                .GroupBy(t => t.SeatClassId)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            var seatClassNames = await db.SeatClasses
                .ToDictionaryAsync(s => s.SeatClassId, s => s.DisplayName);

            var result = new Dictionary<string, int>();

            foreach (var cfg in totalSeats)
            {
                int b = booked.ContainsKey(cfg.SeatClassId) ? booked[cfg.SeatClassId] : 0;
                result[seatClassNames[cfg.SeatClassId]] = cfg.SeatCount - b;
            }

            return result;
        }




    }
}
