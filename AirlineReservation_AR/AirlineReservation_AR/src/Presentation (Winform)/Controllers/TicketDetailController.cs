using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class TicketDetailController
    {
        private readonly IRescheduleService _rescheduleService;
        private readonly IFareRuleService _fareRuleService;

        // Constructor Injection đúng theo yêu cầu
        public TicketDetailController(
            IRescheduleService rescheduleService,
            IFareRuleService fareRuleService)
        {
            _rescheduleService = rescheduleService;
            _fareRuleService = fareRuleService;
        }

        /// <summary>
        /// Check xem vé có được đổi không (Status, Time limit...)
        /// </summary>
        public async Task<RescheduleEligibilityResult> GetRescheduleEligibilityAsync(Guid ticketId)
        {
            try
            {
                // Gọi thẳng service bạn đã viết logic
                return await _rescheduleService.CheckEligibilityAsync(ticketId);
            }
            catch (Exception ex)
            {
                return new RescheduleEligibilityResult
                {
                    CanReschedule = false,
                    Reason = $"Controller Error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Lấy báo giá chênh lệch.
        /// Logic: Map tham số rời rạc -> DTO Request -> Gọi Service
        /// </summary>
        public async Task<RescheduleQuoteResultDto> GetRescheduleQuoteAsync(Guid ticketId, int newFlightId)
        {
            var request = new RescheduleQuoteRequestDto
            {
                TicketId = ticketId,
                NewFlightId = newFlightId,
                RequestedAt = DateTime.Now
            };

            return await _rescheduleService.GetQuoteAsync(request);
        }

        /// <summary>
        /// Thực hiện đổi vé chính thức (Confirm)
        /// </summary>
        public async Task<RescheduleResultDto> RescheduleAsync(Guid ticketId, int newFlightId)
        {
            var request = new RescheduleQuoteRequestDto
            {
                TicketId = ticketId,
                NewFlightId = newFlightId,
                RequestedAt = DateTime.Now
            };

            return await _rescheduleService.ConfirmRescheduleAsync(request);
        }

        /// <summary>
        /// Check hoàn vé dựa trên FareRule (Simple check)
        /// </summary>
        public async Task<bool> CanRefundAsync(Guid ticketId)
        {
            try
            {
                // Vì IFareRuleService chưa có hàm IsRefundableAsync, ta lấy Rule ra để check property
                var rule = await _fareRuleService.GetFareRuleForTicketAsync(ticketId);

                // Giả định Entity FareRule của bạn có thuộc tính AllowRefund (bool)
                // Nếu chưa có, hãy thêm vào Entity hoặc dùng logic: return rule != null;
                return rule != null && rule.AllowRefund;
            }
            catch
            {
                return false;
            }
        }
    }
}
