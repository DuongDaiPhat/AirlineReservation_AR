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


            // 2) OUTBOUND FLIGHTS
            var outboundFlights = await LoadFlightsAsync(db, p.FromCode, p.ToCode, p.StartDate!.Value);

            // 3) RETURN FLIGHTS (optional)
            List<Flight>? returnFlights = null;
            if (p.ReturnDate != null)
            {
                returnFlights = await LoadFlightsAsync(db, p.ToCode, p.FromCode, p.ReturnDate.Value);
            }

            // 4) Convert outbound flights
            var outboundResult = await ConvertToResultDTO(outboundFlights, p.SeatClassId, db);
            decimal lowest = outboundResult.Any()
                    ? outboundResult.Min(f => f.Price)
                    : 0;

            // 5) Convert return flights

            var dayTabsReturn = new List<FlightDayPriceDTO>();
            var sevenDayPricesReturn = new List<FlightDayPriceDTO>();
            List<FlightResultDTO> returnResult = new();
            if (returnFlights != null)
            {
                returnResult = await ConvertToResultDTO(returnFlights, p.SeatClassId, db);

                sevenDayPricesReturn = await Get7DayPricesAsync(db, p.ToCode, p.FromCode, p.ReturnDate.Value, p.SeatClassId);
            }

            // 6) Build airline filters (from outbound only)
            var airlineFilters = outboundResult
                .GroupBy(f => f.AirlineName)
                .Select(g => new AirlineFilterDTO
                {
                    AirlineName = g.Key,
                    LogoUrl = g.First().AirlineLogo
                })
                .ToList();
            
            var retrunAirlineFilters = returnResult
                .GroupBy(f => f.AirlineName)
                .Select(g => new AirlineFilterDTO
                {
                    AirlineName = g.Key,
                    LogoUrl = g.First().AirlineLogo
                })
                .ToList();
            // 1) Build day tabs (only for start date)
            var sevenDayPrices = await Get7DayPricesAsync(db, p.FromCode, p.ToCode, p.StartDate.Value, p.SeatClassId);

            return new FlightSearchResultDTO
            {
                OutboundFlights = outboundResult,
                ReturnFlights = returnResult,
                AirlineFilters = airlineFilters,
                RetrunAirlineFilters = retrunAirlineFilters,
                DayTabs = sevenDayPrices,
                RoundTrip = p.RoundTrip,
                DayTabReturn = sevenDayPricesReturn
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


        private async Task<List<FlightResultDTO>> ConvertToResultDTO(List<Flight> flights, int seatClassId, AirlineReservationDbContext db)
        {
            if (!flights.Any()) return new List<FlightResultDTO>();

            string selectedSeatClassName = await db.SeatClasses
                .Where(s => s.SeatClassId == seatClassId)
                .Select(s => s.DisplayName)
                .FirstAsync();

            var seatClassMap = await db.SeatClasses
                .ToDictionaryAsync(s => s.SeatClassId, s => s.DisplayName);

            var result = new List<FlightResultDTO>();

            foreach (var f in flights)
            {
                var seatsLeft = await GetSeatAvailabilityAsync(f.FlightId);

                seatsLeft.TryGetValue(selectedSeatClassName, out int available);

                var priceRow = f.FlightPricings.FirstOrDefault(x => x.SeatClassId == seatClassId);

                result.Add(new FlightResultDTO
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

                    AircraftType = f.Aircraft.AircraftType.DisplayName,
                    FlightCode = f.FlightNumber
                });
            }

            return result.OrderBy(f => f.Price).ToList();
        }

        private async Task<List<Flight>> LoadFlightsAsync(
            AirlineReservationDbContext db,
            string fromCode,
            string toCode,
            DateTime date)
        {
            var fromAirportId = await db.Airports
                .Where(a => a.IataCode == fromCode)
                .Select(a => a.AirportId)
                .FirstAsync();

            var toAirportId = await db.Airports
                .Where(a => a.IataCode == toCode)
                .Select(a => a.AirportId)
                .FirstAsync();

            return await db.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft).ThenInclude(a => a.AircraftType)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.FlightPricings)
                .Where(f =>
                    f.DepartureAirportId == fromAirportId &&
                    f.ArrivalAirportId == toAirportId &&
                    f.FlightDate == date)
                .ToListAsync();
        }

        private List<FlightDayPriceDTO> BuildDayTabs(DateTime start, decimal lowestPrice)
        {
            var list = new List<FlightDayPriceDTO>();

            for (int offset = -3; offset <= 3; offset++)
            {
                var date = start.AddDays(offset);

                list.Add(new FlightDayPriceDTO
                {
                    Date = date,
                    LowestPrice = (offset == 0 ? lowestPrice : 0)
                });
            }

            return list;
        }

        private async Task<List<FlightDayPriceDTO>> Get7DayPricesAsync(
            AirlineReservationDbContext db,
            string fromCode,
            string toCode,
            DateTime startDate,
            int seatClassId)
        {
            var list = new List<FlightDayPriceDTO>();

            var fromAirportId = await db.Airports
                .Where(a => a.IataCode == fromCode)
                .Select(a => a.AirportId)
                .FirstAsync();

            var toAirportId = await db.Airports
                .Where(a => a.IataCode == toCode)
                .Select(a => a.AirportId)
                .FirstAsync();

            for (int offset = -3; offset <= 3; offset++)
            {
                var date = startDate.AddDays(offset);

                var query = await db.Flights
                    .Where(f =>
                        f.DepartureAirportId == fromAirportId &&
                        f.ArrivalAirportId == toAirportId &&
                        f.FlightDate == date)
                    .Select(f => new
                    {
                        BasePrice = f.BasePrice,
                        ExtraPrice = f.FlightPricings
                            .Where(x => x.SeatClassId == seatClassId)
                            .Select(x => x.Price)
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                decimal lowestPrice = 0;

                if (query.Any())
                {
                    lowestPrice = query.Min(x => x.ExtraPrice == 0 ? x.BasePrice : x.ExtraPrice);
                }

                list.Add(new FlightDayPriceDTO
                {
                    Date = date,
                    LowestPrice = lowestPrice
                });
            }

            return list;
        }





    }
}