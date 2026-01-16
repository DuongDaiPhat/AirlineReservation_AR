using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class RescheduleResultDto
    {
        // True: Đổi vé thành công. False: Thất bại.
        public bool IsSuccess { get; set; }

        // Lý do thất bại (vd: Lỗi thanh toán, lỗi DB concurrency...)
        public string? ErrorMessage { get; set; }

        // ID của yêu cầu đổi vé vừa tạo (RescheduleRequest.Id)
        public int? RescheduleRequestId { get; set; }

        // ID vé (có thể giữ nguyên hoặc tạo vé mới tùy logic, ở đây trỏ về vé cũ đã update status)
        public Guid? TicketId { get; set; }

        // ID chuyến bay cũ
        public int? OldFlightId { get; set; }

        // ID chuyến bay mới
        public int? NewFlightId { get; set; }
    }
}
