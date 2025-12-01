using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class RescheduleController
    {
        private readonly IRescheduleService _rescheduleService;

        public RescheduleController(IRescheduleService rescheduleService)
        {
            _rescheduleService = rescheduleService;
        }

        /// <summary>
        /// Thực hiện đổi vé và trả về thông báo text (Success/Fail message)
        /// </summary>
        public async Task<string> TryRescheduleTicket(Guid ticketId, int newFlightId)
        {
            var request = new RescheduleQuoteRequestDto
            {
                TicketId = ticketId,
                NewFlightId = newFlightId,
                RequestedAt = DateTime.Now
            };

            var result = await _rescheduleService.ConfirmRescheduleAsync(request);

            if (result.IsSuccess)
            {
                return "Reschedule Successful! A new ticket has been issued.";
            }
            else
            {
                return $"Failed to reschedule.\nReason: {result.ErrorMessage}";
            }
        }

        /// <summary>
        /// Lấy báo giá và format thành chuỗi đẹp để hiện MessageBox xác nhận tiền
        /// </summary>
        public async Task<string> GetQuoteSummary(Guid ticketId, int newFlightId)
        {
            var request = new RescheduleQuoteRequestDto
            {
                TicketId = ticketId,
                NewFlightId = newFlightId,
                RequestedAt = DateTime.Now
            };

            var quote = await _rescheduleService.GetQuoteAsync(request);

            if (!quote.IsSuccess)
            {
                return $"Error calculating quote: {quote.ErrorMessage}";
            }

            // Format tiền tệ chuẩn N0 (có dấu phẩy ngăn cách)
            // Ví dụ: 1,000,000 VND
            return $"--- PAYMENT SUMMARY ---\n\n" +
                   $"Old Ticket Price:    {quote.OriginalBasePrice:N0} {quote.Currency}\n" +
                   $"New Ticket Price:    {quote.NewBasePrice:N0} {quote.Currency}\n" +
                   $"------------------------------\n" +
                   $"Fare Difference:     {quote.FareDifference:N0} {quote.Currency}\n" +
                   $"Change Fee (Penalty):{quote.PenaltyFee:N0} {quote.Currency}\n" +
                   $"------------------------------\n" +
                   $"TOTAL TO PAY:        {quote.TotalAmount:N0} {quote.Currency}";
        }
        public async Task<List<AvailableFlightDto>> SearchAvailableFlightsForRescheduleAsync(Guid ticketId, DateTime newDate)
        {
            return await _rescheduleService.SearchAvailableFlightsForRescheduleAsync(ticketId, newDate);
        }
    }
}
