using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PaymentRequestDTO
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = "MOMO";
    }
}
