using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    /// <summary>
    /// DTO for status dropdown selection (booking, payment, flight statuses)
    /// </summary>
    public class StatusSelectDto
    {
        public string Code { get; set; } = string.Empty;  // "Confirmed", "Pending", etc.
        public string DisplayName { get; set; } = string.Empty;  // English display name
        
        public override string ToString() => DisplayName;
        
        public override bool Equals(object? obj)
        {
            if (obj is StatusSelectDto other)
                return Code == other.Code;
            return false;
        }
        
        public override int GetHashCode() => Code?.GetHashCode() ?? 0;
    }
}
