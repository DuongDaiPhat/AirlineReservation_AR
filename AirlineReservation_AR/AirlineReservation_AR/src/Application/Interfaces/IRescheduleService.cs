using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IRescheduleService
    {
        /// <summary>
        /// Kiểm tra điều kiện đổi vé để hiển thị UI (Enable/Disable button)
        /// </summary>
        Task<RescheduleEligibilityResult> CheckEligibilityAsync(Guid ticketId);

        /// <summary>
        /// Tính toán báo giá đổi vé (Chênh lệch + Phí phạt)
        /// </summary>
        Task<RescheduleQuoteResultDto> GetQuoteAsync(RescheduleQuoteRequestDto request);

        /// <summary>
        /// Xác nhận đổi vé: Tạo Request, Thanh toán, Cập nhật vé
        /// </summary>
        Task<RescheduleResultDto> ConfirmRescheduleAsync(RescheduleQuoteRequestDto request);
    }
}
