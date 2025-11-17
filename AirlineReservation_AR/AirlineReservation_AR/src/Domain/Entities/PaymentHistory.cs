using System;

namespace AirlineReservation.src.AirlineReservation.Domain.Entities
{
    public class PaymentHistory
    {
        public int HistoryId { get; set; }
        public int PaymentId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime TransactionTime { get; set; }
        public string? Note { get; set; }

        public Payment Payment { get; set; } = null!;
    }
}
