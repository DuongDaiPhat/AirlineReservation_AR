using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PaymentResultDTO
    {
        public bool Success { get; set; }
        public string QrUrl { get; set; }
        public string Message { get; set; }
    }
}
