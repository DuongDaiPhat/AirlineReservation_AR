using System;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class AuditLog
    {
        public long LogId { get; set; }
        public Guid? UserId { get; set; }
        public string TableName { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public string? RecordId { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public DateTime Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public User? User { get; set; }
    }
}
