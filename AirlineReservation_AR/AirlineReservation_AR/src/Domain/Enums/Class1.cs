using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR_AirlineReservation_AR.src.Domain.Enums
{
    internal class Class1
    {
    }
    public enum TicketStatus
    {
        Issued,         // Đã xuất vé
        CheckedIn,      // Đã check-in online/tại quầy
        Boarded,        // Đã lên máy bay
        Cancelled,      // Đã hủy
        Refunded,       // Đã hoàn tiền
        Rescheduled     // Đã đổi vé
    }

    public enum BookingFlightStatus
    {
        Booked,         // Đã đặt
        Rescheduled,    // Đã bị đổi sang chuyến khác
        Cancelled       // Đã hủy
    }
}
