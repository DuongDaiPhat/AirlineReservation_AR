using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class BookingFilterDto
    {
        public string? BookingReference { get; set; }
        public string? EmailOrPhone { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BookingStatus { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
