using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class ReportServiceAdmin : IReportServiceAdmin
    {
        private readonly AirlineReservationDbContext _context;
        public ReportServiceAdmin(AirlineReservationDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardStatisticsDtoAdmin> GetDashboardStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            var currentPeriodBookings = await GetBookingsInPeriod(fromDate, toDate);
            var previousPeriodBookings = await GetBookingsInPeriod(
                fromDate.AddDays(-(toDate - fromDate).Days),
                fromDate.AddDays(-1)
            );

            var currentRevenue = CalculateTotalRevenue(currentPeriodBookings);
            var previousRevenue = CalculateTotalRevenue(previousPeriodBookings);

            var currentCustomers = currentPeriodBookings.Select(b => b.UserId).Distinct().Count();
            var previousCustomers = previousPeriodBookings.Select(b => b.UserId).Distinct().Count();

            var currentFlights = currentPeriodBookings
                .SelectMany(b => b.BookingFlights)
                .Select(bf => bf.FlightId)
                .Distinct()
                .Count();

            var previousFlights = previousPeriodBookings
                .SelectMany(b => b.BookingFlights)
                .Select(bf => bf.FlightId)
                .Distinct()
                .Count();

            return new DashboardStatisticsDtoAdmin
            {
                TotalRevenue = currentRevenue,
                TotalBookings = currentPeriodBookings.Count,
                TotalFlights = currentFlights,
                TotalCustomers = currentCustomers,
                AverageTicketPrice = currentPeriodBookings.Any() ? currentRevenue / currentPeriodBookings.Count : 0,
                RevenueGrowthRate = CalculateGrowthRate(currentRevenue, previousRevenue),
                BookingGrowthRate = CalculateGrowthRate(currentPeriodBookings.Count, previousPeriodBookings.Count),
                FlightGrowthRate = CalculateGrowthRate(currentFlights, previousFlights),
                CustomerGrowthRate = CalculateGrowthRate(currentCustomers, previousCustomers),
                PriceChangeRate = CalculateGrowthRate(
                    currentPeriodBookings.Any() ? currentRevenue / currentPeriodBookings.Count : 0,
                    previousPeriodBookings.Any() ? previousRevenue / previousPeriodBookings.Count : 0
                )
            };
        }

        public async Task<List<StatCardDtoAdmin>> GetStatCardsAsync(DateTime fromDate, DateTime toDate)
        {
            var stats = await GetDashboardStatisticsAsync(fromDate, toDate);

            return new List<StatCardDtoAdmin>
            {
                new StatCardDtoAdmin
                {
                    Icon = "💰",
                    Title = "Tổng doanh thu",
                    Value = FormatCurrency(stats.TotalRevenue),
                    Change = FormatPercentage(stats.RevenueGrowthRate),
                    IsPositive = stats.RevenueGrowthRate >= 0
                },
                new StatCardDtoAdmin
                {
                    Icon = "🎫",
                    Title = "Tổng booking",
                    Value = stats.TotalBookings.ToString("N0"),
                    Change = FormatPercentage(stats.BookingGrowthRate),
                    IsPositive = stats.BookingGrowthRate >= 0
                },
                new StatCardDtoAdmin
                {
                    Icon = "✈️",
                    Title = "Chuyến bay",
                    Value = stats.TotalFlights.ToString(),
                    Change = stats.FlightGrowthRate >= 0 ? $"+{stats.FlightGrowthRate:N0}" : $"{stats.FlightGrowthRate:N0}",
                    IsPositive = stats.FlightGrowthRate >= 0
                },
                new StatCardDtoAdmin
                {
                    Icon = "👥",
                    Title = "Khách hàng",
                    Value = stats.TotalCustomers.ToString("N0"),
                    Change = FormatPercentage(stats.CustomerGrowthRate),
                    IsPositive = stats.CustomerGrowthRate >= 0
                },
                new StatCardDtoAdmin
                {
                    Icon = "📈",
                    Title = "Giá vé TB",
                    Value = FormatCurrency(stats.AverageTicketPrice),
                    Change = FormatPercentage(stats.PriceChangeRate),
                    IsPositive = stats.PriceChangeRate >= 0
                }
            };
        }

        public async Task<List<MonthlyRevenueDtoAdmin>> GetMonthlyRevenueAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await GetBookingsInPeriod(fromDate, toDate);

            var monthlyData = bookings
                .GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
                .Select(g => new MonthlyRevenueDtoAdmin
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(b => b.Payments.Where(p => p.Status == "Completed").Sum(p => p.Amount)),
                    BookingCount = g.Count(),
                    MonthLabel = $"T{g.Key.Month}"
                })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ToList();

            return monthlyData;
        }

        public async Task<List<RevenueByRouteDtoAdmin>> GetRevenueByRouteAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await _context.Bookings
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.Payments)
                .Where(b => b.BookingDate.Date >= fromDate.Date && b.BookingDate.Date <= toDate.Date)
                .AsNoTracking()
                .ToListAsync();

            var routeData = bookings
                .SelectMany(b => b.BookingFlights.Select(bf => new
                {
                    Route = $"{bf.Flight.DepartureAirport.IataCode} → {bf.Flight.ArrivalAirport.IataCode}",
                    DepartureAirport = bf.Flight.DepartureAirport.IataCode,
                    ArrivalAirport = bf.Flight.ArrivalAirport.IataCode,
                    Revenue = b.Payments.Where(p => p.Status == "Completed").Sum(p => p.Amount),
                    BookingId = b.BookingId
                }))
                .GroupBy(x => new { x.Route, x.DepartureAirport, x.ArrivalAirport })
                .Select(g => new RevenueByRouteDtoAdmin
                {
                    Route = g.Key.Route,
                    DepartureAirport = g.Key.DepartureAirport,
                    ArrivalAirport = g.Key.ArrivalAirport,
                    BookingCount = g.Select(x => x.BookingId).Distinct().Count(),
                    TotalRevenue = g.Sum(x => x.Revenue),
                    AveragePrice = g.Any() ? g.Sum(x => x.Revenue) / g.Select(x => x.BookingId).Distinct().Count() : 0
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToList();

            return routeData;
        }

        public async Task<List<RevenueByAirlineDtoAdmin>> GetRevenueByAirlineAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await _context.Bookings
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Airline)
                .Include(b => b.Payments)
                .Where(b => b.BookingDate.Date >= fromDate.Date && b.BookingDate.Date <= toDate.Date)
                .AsNoTracking()
                .ToListAsync();

            var airlineData = bookings
                .SelectMany(b => b.BookingFlights.Select(bf => new
                {
                    AirlineName = bf.Flight.Airline.AirlineName,
                    IataCode = bf.Flight.Airline.IataCode,
                    FlightId = bf.FlightId,
                    Revenue = b.Payments.Where(p => p.Status == "Completed").Sum(p => p.Amount),
                    BookingId = b.BookingId
                }))
                .GroupBy(x => new { x.AirlineName, x.IataCode })
                .Select(g => new RevenueByAirlineDtoAdmin
                {
                    AirlineName = g.Key.AirlineName,
                    IataCode = g.Key.IataCode,
                    TotalRevenue = g.Sum(x => x.Revenue),
                    FlightCount = g.Select(x => x.FlightId).Distinct().Count(),
                    BookingCount = g.Select(x => x.BookingId).Distinct().Count()
                })
                .OrderByDescending(a => a.TotalRevenue)
                .ToList();

            return airlineData;
        }


        public async Task<List<TopRouteDtoAdmin>> GetTopRoutesAsync(DateTime fromDate, DateTime toDate, int topN = 5)
        {
            var routes = await GetRevenueByRouteAsync(fromDate, toDate);
            var totalRevenue = routes.Sum(r => r.TotalRevenue);

            return routes
                .Take(topN)
                .Select((r, index) => new TopRouteDtoAdmin
                {
                    Rank = index + 1,
                    Route = r.Route,
                    DepartureCode = r.DepartureAirport,
                    ArrivalCode = r.ArrivalAirport,
                    BookingCount = r.BookingCount,
                    TotalRevenue = r.TotalRevenue,
                    MarketShare = totalRevenue > 0 ? (r.TotalRevenue / totalRevenue) * 100 : 0
                })
                .ToList();
        }

        public async Task<List<TopCustomerDtoAdmin>> GetTopCustomersAsync(DateTime fromDate, DateTime toDate, int topN = 5)
        {
            var bookings = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Payments)
                .Where(b => b.BookingDate.Date >= fromDate.Date && b.BookingDate.Date <= toDate.Date)
                .AsNoTracking()
                .ToListAsync();

            var customerData = bookings
                .GroupBy(b => b.User)
                .Select(g => new TopCustomerDtoAdmin
                {
                    UserId = g.Key.UserId,
                    CustomerName = g.Key.FullName,
                    Email = g.Key.Email,
                    Phone = g.Key.Phone,
                    BookingCount = g.Count(),
                    TotalSpent = g.Sum(b => b.Payments.Where(p => p.Status == "Completed").Sum(p => p.Amount)),
                    LastBookingDate = g.Max(b => b.BookingDate)
                })
                .OrderByDescending(c => c.TotalSpent)
                .Take(topN)
                .Select((c, index) => new TopCustomerDtoAdmin
                {
                    Rank = index + 1,
                    UserId = c.UserId,
                    CustomerName = c.CustomerName,
                    Email = c.Email,
                    Phone = c.Phone,
                    BookingCount = c.BookingCount,
                    TotalSpent = c.TotalSpent,
                    LastBookingDate = c.LastBookingDate
                })
                .ToList();

            return customerData;
        }

        public async Task<List<BookingStatusDtoAdmin>> GetBookingStatusAnalysisAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await GetBookingsInPeriod(fromDate, toDate);
            var totalBookings = bookings.Count;

            var statusData = bookings
                .GroupBy(b => b.Status ?? "Unknown")
                .Select(g => new BookingStatusDtoAdmin
                {
                    Status = g.Key,
                    Count = g.Count(),
                    Percentage = totalBookings > 0 ? (decimal)g.Count() / totalBookings * 100 : 0
                })
                .OrderByDescending(s => s.Count)
                .ToList();

            return statusData;
        }

        public async Task<List<BookingTrendDtoAdmin>> GetBookingTrendsAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await GetBookingsInPeriod(fromDate, toDate);

            var trendData = bookings
                .GroupBy(b => b.BookingDate.Date)
                .Select(g => new BookingTrendDtoAdmin
                {
                    Date = g.Key,
                    BookingCount = g.Count(),
                    ConfirmedCount = g.Count(b => b.Status == "Confirmed"),
                    CancelledCount = g.Count(b => b.Status == "Cancelled"),
                    CancellationRate = g.Any() ? (decimal)g.Count(b => b.Status == "Cancelled") / g.Count() * 100 : 0
                })
                .OrderBy(t => t.Date)
                .ToList();

            return trendData;
        }

        public async Task<List<FlightPerformanceDtoAdmin>> GetFlightPerformanceAsync(DateTime fromDate, DateTime toDate)
        {
            var flights = await _context.Flights
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.Aircraft)
                .Include(f => f.BookingFlights)
                    .ThenInclude(bf => bf.Booking)
                        .ThenInclude(b => b.Payments)
                .Where(f => f.FlightDate.Date >= fromDate.Date && f.FlightDate.Date <= toDate.Date)
                .AsNoTracking()
                .ToListAsync();

            var performanceData = flights.Select(f => new FlightPerformanceDtoAdmin
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNumber,
                Route = $"{f.DepartureAirport.IataCode} → {f.ArrivalAirport.IataCode}",
                TotalSeats = f.Aircraft.Seats.Count,
                BookedSeats = f.BookingFlights.Count,
                OccupancyRate = (f.Aircraft.Seats.Count) > 0
                    ? (decimal)f.BookingFlights.Count / (f.Aircraft.Seats.Count) * 100
                    : 0,
                Revenue = f.BookingFlights.Sum(bf =>
                    bf.Booking.Payments.Where(p => p.Status == "Completed").Sum(p => p.Amount))
            })
            .OrderByDescending(p => p.Revenue)
            .ToList();

            return performanceData;
        }

        public async Task<ReportSummaryDtoAdmin> GetReportSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            var bookings = await GetBookingsInPeriod(fromDate, toDate);
            var totalRevenue = CalculateTotalRevenue(bookings);

            var totalFlights = bookings
                .SelectMany(b => b.BookingFlights)
                .Select(bf => bf.FlightId)
                .Distinct()
                .Count();

            var totalPassengers = await _context.Passengers
                .Where(p => bookings.Select(b => b.BookingId).Contains(p.BookingId))
                .CountAsync();

            var cancelledBookings = bookings.Count(b => b.Status == "Cancelled");
            var cancellationRate = bookings.Any() ? (decimal)cancelledBookings / bookings.Count * 100 : 0;

            return new ReportSummaryDtoAdmin
            {
                TotalFlights = totalFlights,
                TotalPassengers = totalPassengers,
                TotalRevenue = totalRevenue,
                TotalProfit = totalRevenue * 0.25m, // Giả sử lợi nhuận 25%
                CancellationRate = cancellationRate,
                AverageSatisfactionScore = 4.7m, // Giá trị giả định
                GeneratedAt = DateTime.Now,
                FromDate = fromDate,
                ToDate = toDate
            };
        }

        public async Task<ComprehensiveReportDtoAdmin> GetComprehensiveReportAsync(ReportRequestDtoAdmin request)
        {
            var report = new ComprehensiveReportDtoAdmin
            {
                Statistics = await GetDashboardStatisticsAsync(request.FromDate, request.ToDate),
                MonthlyRevenue = await GetMonthlyRevenueAsync(request.FromDate, request.ToDate),
                TopRoutes = await GetTopRoutesAsync(request.FromDate, request.ToDate, request.TopN),
                TopCustomers = await GetTopCustomersAsync(request.FromDate, request.ToDate, request.TopN),
                BookingStatuses = await GetBookingStatusAnalysisAsync(request.FromDate, request.ToDate),
                Summary = await GetReportSummaryAsync(request.FromDate, request.ToDate)
            };

            return report;
        }

        public async Task<byte[]> ExportReportToExcelAsync(ReportRequestDtoAdmin request)
        {
            // TODO: Implement Excel export using EPPlus or ClosedXML
            // This is a placeholder
            await Task.CompletedTask;
            return Array.Empty<byte>();
        }


        private async Task<List<AirlineReservation.Domain.Entities.Booking>> GetBookingsInPeriod(DateTime fromDate, DateTime toDate)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Payments)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.DepartureAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.ArrivalAirport)
                .Include(b => b.BookingFlights)
                    .ThenInclude(bf => bf.Flight)
                        .ThenInclude(f => f.Airline)
                .Where(b => b.BookingDate.Date >= fromDate.Date && b.BookingDate.Date <= toDate.Date)
                .AsNoTracking()
                .ToListAsync();
        }

        private decimal CalculateTotalRevenue(List<AirlineReservation.Domain.Entities.Booking> bookings)
        {
            return bookings.Sum(b => b.Payments
                .Where(p => p.Status == "Completed")
                .Sum(p => p.Amount));
        }

        private decimal CalculateGrowthRate(decimal current, decimal previous)
        {
            if (previous == 0) return current > 0 ? 100 : 0;
            return ((current - previous) / previous) * 100;
        }

        private decimal CalculateGrowthRate(int current, int previous)
        {
            if (previous == 0) return current > 0 ? 100 : 0;
            return ((decimal)(current - previous) / previous) * 100;
        }

        private string FormatCurrency(decimal amount)
        {
            if (amount >= 1_000_000_000)
                return $"₫{amount / 1_000_000_000:N1} tỷ";
            if (amount >= 1_000_000)
                return $"₫{amount / 1_000_000:N1}M";
            return $"₫{amount:N0}";
        }

        private string FormatPercentage(decimal percentage)
        {
            var sign = percentage >= 0 ? "+" : "";
            return $"{sign}{percentage:N1}%";
        }
    }
}
