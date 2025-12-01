using AR_AirlineReservation_AR.src.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class TicketBasicInfoDto
    {
        public Guid TicketId { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public string FlightCode { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public string SeatClass { get; set; } = string.Empty; 
        public decimal OriginalPrice { get; set; }
    }
}
