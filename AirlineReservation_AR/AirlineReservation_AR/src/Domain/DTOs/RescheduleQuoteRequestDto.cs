using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class RescheduleQuoteRequestDto
    {
        // ID của vé muốn đổi
        public Guid TicketId { get; set; }

        // ID của chuyến bay mới user đã chọn trên UI tìm kiếm chuyến bay
        public int NewFlightId { get; set; }

        // Thời điểm yêu cầu (thường là DateTime.Now) để validate rule thời gian
        public DateTime RequestedAt { get; set; }
    }
}
