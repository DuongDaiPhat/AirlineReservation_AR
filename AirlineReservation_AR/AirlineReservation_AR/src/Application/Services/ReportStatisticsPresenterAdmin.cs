using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class ReportStatisticsPresenterAdmin
    {
        private readonly IReportServiceAdmin _reportService;
        private readonly IReportStatisticsViewAdmin _view;

        public ReportStatisticsPresenterAdmin(IReportStatisticsViewAdmin view, IReportServiceAdmin reportService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }

        public async Task LoadInitialDataAsync()
        {
            try
            {
                _view.ShowLoading(true);

                var fromDate = DateTime.Now.AddMonths(-1);
                var toDate = DateTime.Now;

                await LoadAllReportDataAsync(fromDate, toDate);

                _view.ShowLoading(false);
            }
            catch (Exception ex)
            {
                _view.ShowLoading(false);
                _view.ShowError($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        public async Task LoadAllReportDataAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Load stat cards
                var statCards = await _reportService.GetStatCardsAsync(fromDate, toDate);
                _view.DisplayStatCards(statCards);

                // Load monthly revenue
                var monthlyRevenue = await _reportService.GetMonthlyRevenueAsync(fromDate, toDate);
                _view.DisplayRevenueChart(monthlyRevenue);

                // Load top routes
                var topRoutes = await _reportService.GetTopRoutesAsync(fromDate, toDate, 5);
                _view.DisplayTopRoutes(topRoutes);

                // Load top customers
                var topCustomers = await _reportService.GetTopCustomersAsync(fromDate, toDate, 5);
                _view.DisplayTopCustomers(topCustomers);

                // Load summary
                var summary = await _reportService.GetReportSummaryAsync(fromDate, toDate);
                _view.DisplaySummary(summary);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi khi tải báo cáo: {ex.Message}");
            }
        }

        public async Task ApplyFilterAsync(string reportType, DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    _view.ShowWarning("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!");
                    return;
                }

                _view.ShowLoading(true);

                switch (reportType)
                {
                    case "Doanh thu":
                        await LoadRevenueReportAsync(fromDate, toDate);
                        break;
                    case "Booking":
                        await LoadBookingReportAsync(fromDate, toDate);
                        break;
                    case "Khách hàng":
                        await LoadCustomerReportAsync(fromDate, toDate);
                        break;
                    case "Chuyến bay":
                        await LoadFlightReportAsync(fromDate, toDate);
                        break;
                    default:
                        await LoadAllReportDataAsync(fromDate, toDate);
                        break;
                }

                _view.ShowLoading(false);
                _view.ShowSuccess($"Đã tải báo cáo {reportType} từ {fromDate:dd/MM/yyyy} đến {toDate:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                _view.ShowLoading(false);
                _view.ShowError($"Lỗi khi lọc dữ liệu: {ex.Message}");
            }
        }

        private async System.Threading.Tasks.Task LoadRevenueReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var statCards = await reportController.GetStatCardsAsync(fromDate, toDate);
            _view.DisplayStatCards(statCards);

            var revenueByRoute = await reportController.GetRevenueByRouteAsync(fromDate, toDate);
            _view.DisplayRevenueByRoute(revenueByRoute);

            var monthlyRevenue = await reportController.GetMonthlyRevenueAsync(fromDate, toDate);
            _view.DisplayRevenueChart(monthlyRevenue);
        }

        private async System.Threading.Tasks.Task LoadBookingReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var bookingStatuses = await reportController.GetBookingStatusAnalysisAsync(fromDate, toDate);
            // TODO: Display booking statuses chart

            var bookingTrends = await reportController.GetBookingTrendsAsync(fromDate, toDate);
            // TODO: Display booking trends chart
        }

        private async System.Threading.Tasks.Task LoadCustomerReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var topCustomers = await reportController.GetTopCustomersAsync(fromDate, toDate);
            _view.DisplayTopCustomers(topCustomers);
        }

        private async System.Threading.Tasks.Task LoadFlightReportAsync(DateTime fromDate, DateTime toDate)
        {
            var reportController = DIContainer.ReportControllerAdmin;

            var flightPerformance = await reportController.GetFlightPerformanceAsync(fromDate, toDate);
            // TODO: Display flight performance
        }

        public async Task ExportReportAsync(string reportType, DateTime fromDate, DateTime toDate, string filePath)
        {
            try
            {
                _view.ShowLoading(true);

                var request = new Domain.DTOs.ReportRequestDtoAdmin
                {
                    ReportType = reportType,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                var excelData = await _reportService.ExportReportToExcelAsync(request);

                if (excelData != null && excelData.Length > 0)
                {
                    await File.WriteAllBytesAsync(filePath, excelData);
                    _view.ShowSuccess($"Đã xuất báo cáo thành công!\n{filePath}");
                }
                else
                {
                    _view.ShowWarning("Chức năng xuất Excel đang được phát triển.");
                }

                _view.ShowLoading(false);
            }
            catch (Exception ex)
            {
                _view.ShowLoading(false);
                _view.ShowError($"Lỗi khi xuất báo cáo: {ex.Message}");
            }
        }

        public async Task RefreshDataAsync()
        {
            await LoadInitialDataAsync();
        }

        public void ClearData()
        {
            _view.ClearAllData();
        }
    }
}
