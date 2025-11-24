using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class ReportControllerAdmin
    {
        private readonly IReportServiceAdmin _reportService;

        public ReportControllerAdmin(IReportServiceAdmin reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        public async Task<DashboardStatisticsDtoAdmin> GetDashboardStatisticsAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetDashboardStatisticsAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê dashboard: {ex.Message}", ex);
            }
        }

        public async Task<List<StatCardDtoAdmin>> GetStatCardsAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetStatCardsAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy stat cards: {ex.Message}", ex);
            }
        }

        public async Task<List<MonthlyRevenueDtoAdmin>> GetMonthlyRevenueAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetMonthlyRevenueAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy doanh thu theo tháng: {ex.Message}", ex);
            }
        }

        public async Task<List<RevenueByRouteDtoAdmin>> GetRevenueByRouteAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetRevenueByRouteAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy doanh thu theo tuyến: {ex.Message}", ex);
            }
        }

        public async Task<List<RevenueByAirlineDtoAdmin>> GetRevenueByAirlineAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetRevenueByAirlineAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy doanh thu theo hãng: {ex.Message}", ex);
            }
        }

        public async Task<List<TopRouteDtoAdmin>> GetTopRoutesAsync(DateTime fromDate, DateTime toDate, int topN = 5)
        {
            try
            {
                if (topN <= 0 || topN > 100)
                {
                    throw new ArgumentException("TopN phải nằm trong khoảng 1-100");
                }

                return await _reportService.GetTopRoutesAsync(fromDate, toDate, topN);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy top routes: {ex.Message}", ex);
            }
        }

        public async Task<List<TopCustomerDtoAdmin>> GetTopCustomersAsync(DateTime fromDate, DateTime toDate, int topN = 5)
        {
            try
            {
                if (topN <= 0 || topN > 100)
                {
                    throw new ArgumentException("TopN phải nằm trong khoảng 1-100");
                }

                return await _reportService.GetTopCustomersAsync(fromDate, toDate, topN);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy top customers: {ex.Message}", ex);
            }
        }

        public async Task<List<BookingStatusDtoAdmin>> GetBookingStatusAnalysisAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetBookingStatusAnalysisAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi phân tích booking status: {ex.Message}", ex);
            }
        }

        public async Task<List<BookingTrendDtoAdmin>> GetBookingTrendsAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetBookingTrendsAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy xu hướng booking: {ex.Message}", ex);
            }
        }

        public async Task<List<FlightPerformanceDtoAdmin>> GetFlightPerformanceAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetFlightPerformanceAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy hiệu suất chuyến bay: {ex.Message}", ex);
            }
        }

        public async Task<ReportSummaryDtoAdmin> GetReportSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _reportService.GetReportSummaryAsync(fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy tổng kết báo cáo: {ex.Message}", ex);
            }
        }

        public async Task<ComprehensiveReportDtoAdmin> GetComprehensiveReportAsync(ReportRequestDtoAdmin request)
        {
            try
            {
                // Validate request
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.FromDate > request.ToDate)
                {
                    throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc");
                }

                if (request.TopN <= 0 || request.TopN > 100)
                {
                    request.TopN = 5; // Default value
                }

                return await _reportService.GetComprehensiveReportAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy báo cáo tổng hợp: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> ExportReportToExcelAsync(ReportRequestDtoAdmin request)
        {
            try
            {
                // Validate request
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.FromDate > request.ToDate)
                {
                    throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc");
                }

                return await _reportService.ExportReportToExcelAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xuất báo cáo Excel: {ex.Message}", ex);
            }
        }

        public bool ValidateDateRange(DateTime fromDate, DateTime toDate, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (fromDate > toDate)
            {
                errorMessage = "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc";
                return false;
            }

            if (fromDate > DateTime.Now)
            {
                errorMessage = "Ngày bắt đầu không được lớn hơn ngày hiện tại";
                return false;
            }

            var daysDiff = (toDate - fromDate).Days;
            if (daysDiff > 365)
            {
                errorMessage = "Khoảng thời gian không được vượt quá 365 ngày";
                return false;
            }

            return true;
        }
    }
}
