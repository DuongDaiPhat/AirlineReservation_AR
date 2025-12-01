using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.Entities
{
    public class RescheduleRequest
    {
        public int RescheduleRequestId { get; set; }

        public int BookingId { get; set; }
        public int BookingFlightId { get; set; }   

        public int OldFlightId { get; set; }
        public int NewFlightId { get; set; }

        public DateTime OldDepartureDateTime { get; set; }
        public DateTime NewDepartureDateTime { get; set; }

        public decimal FareDifference { get; set; }
        public decimal PenaltyFee { get; set; }
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Completed";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public Booking Booking { get; set; } = null!;
        public BookingFlight BookingFlight { get; set; } = null!;
    }
}
