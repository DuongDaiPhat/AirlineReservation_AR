using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class TopCustomerDtoAdmin
    {
        public int Rank { get; set; }
        public Guid UserId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int BookingCount { get; set; }
        public decimal TotalSpent { get; set; }
        public DateTime LastBookingDate { get; set; }
    }
}
