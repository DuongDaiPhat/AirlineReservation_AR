using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IReportServiceAdmin
    {
        Task<byte[]> ExportReportToExcelAsync(ReportRequestDtoAdmin request);
        Task<List<BookingStatusDtoAdmin>> GetBookingStatusAnalysisAsync(DateTime fromDate, DateTime toDate);
        Task<List<BookingTrendDtoAdmin>> GetBookingTrendsAsync(DateTime fromDate, DateTime toDate);
        Task<ComprehensiveReportDtoAdmin> GetComprehensiveReportAsync(ReportRequestDtoAdmin request);
        Task<DashboardStatisticsDtoAdmin> GetDashboardStatisticsAsync(DateTime fromDate, DateTime toDate);
        Task<List<FlightPerformanceDtoAdmin>> GetFlightPerformanceAsync(DateTime fromDate, DateTime toDate);
        Task<List<MonthlyRevenueDtoAdmin>> GetMonthlyRevenueAsync(DateTime fromDate, DateTime toDate);
        Task<ReportSummaryDtoAdmin> GetReportSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<List<RevenueByAirlineDtoAdmin>> GetRevenueByAirlineAsync(DateTime fromDate, DateTime toDate);
        Task<List<RevenueByRouteDtoAdmin>> GetRevenueByRouteAsync(DateTime fromDate, DateTime toDate);
        Task<List<StatCardDtoAdmin>> GetStatCardsAsync(DateTime fromDate, DateTime toDate);
        Task<List<TopCustomerDtoAdmin>> GetTopCustomersAsync(DateTime fromDate, DateTime toDate, int topN);
        Task<List<TopRouteDtoAdmin>> GetTopRoutesAsync(DateTime fromDate, DateTime toDate, int topN);

    }
}
