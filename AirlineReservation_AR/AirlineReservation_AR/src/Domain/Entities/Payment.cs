using System;
using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? PaymentProvider { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? Status { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal? RefundedAmount { get; set; }

        public Booking Booking { get; set; } = null!;
        public ICollection<PaymentHistory> PaymentHistories { get; set; } = new HashSet<PaymentHistory>();
    }
}
