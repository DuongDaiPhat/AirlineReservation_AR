using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class RescheduleEligibilityResult
    {
        // True: Nút Reschedule sẽ Enable. False: Disable.
        public bool CanReschedule { get; set; }

        // Lý do không cho đổi (vd: "Quá giờ cho phép đổi", "Vé hạng promo không được đổi"...)
        public string? Reason { get; set; }

        // Thông tin vé tóm tắt (để hiển thị lại nếu cần check logic UI)
        public TicketBasicInfoDto? TicketInfo { get; set; }

        // Có thể bổ sung thêm MinHoursBeforeChange để UI cảnh báo user
        public int? MinHoursBeforeChange { get; set; }
    }
}
