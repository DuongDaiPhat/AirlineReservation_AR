using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class BookingTrendDtoAdmin
    {
        public DateTime Date { get; set; }
        public int BookingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int CancelledCount { get; set; }
        public decimal CancellationRate { get; set; }
    }
}
