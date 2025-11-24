using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class BookingDtoAdmin
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = null!;
        public int PassengerCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public FlightInfoDtoAdmin FlightInfo { get; set; } = null!;
    }
}
