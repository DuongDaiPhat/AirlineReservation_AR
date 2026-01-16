using AirlineReservation_AR.src.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IFareRuleService
    {
        /// <summary>
        /// Lấy quy tắc giá vé dựa trên Chuyến bay và Hạng ghế (dùng khi search vé mới)
        /// </summary>
        Task<FareRule?> GetFareRuleForFlightAsync(int flightId, int seatClassId);

        /// <summary>
        /// Lấy quy tắc giá vé áp dụng cho một vé cụ thể (dùng khi check vé cũ)
        /// </summary>
        Task<FareRule?> GetFareRuleForTicketAsync(Guid ticketId);

        /// <summary>
        /// Kiểm tra nhanh xem vé có được phép đổi hay không (Check AllowChange + Thời gian)
        /// </summary>
        Task<bool> CanChangeAsync(Guid ticketId);
    }
}
