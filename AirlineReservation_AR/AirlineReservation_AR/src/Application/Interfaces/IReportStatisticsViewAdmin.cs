using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IReportStatisticsViewAdmin
    {
        // Display methods
        void DisplayStatCards(List<StatCardDtoAdmin> statCards);
        void DisplayRevenueChart(List<MonthlyRevenueDtoAdmin> monthlyRevenue);
        void DisplayTopRoutes(List<TopRouteDtoAdmin> topRoutes);
        void DisplayTopCustomers(List<TopCustomerDtoAdmin> topCustomers);
        void DisplaySummary(ReportSummaryDtoAdmin summary);
        void DisplayRevenueByRoute(List<RevenueByRouteDtoAdmin> revenueByRoute);
        void DisplayBookingStatuses(List<BookingStatusDtoAdmin> bookingStatuses);
        void DisplayBookingTrends(List<BookingTrendDtoAdmin> bookingTrends);
        void DisplayFlightPerformance(List<FlightPerformanceDtoAdmin> flightPerformance);

        // UI State methods
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowWarning(string message);
        void ShowSuccess(string message);
        void ClearAllData();
    }
}
