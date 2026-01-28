using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    /// <summary>
    /// DTO for airline dropdown selection
    /// </summary>
    public class AirlineSelectDto
    {
        public int AirlineId { get; set; }
        public string Code { get; set; } = string.Empty;  // IATA code (VN, VJ, QH)
        public string DisplayName { get; set; } = string.Empty;  // "Vietnam Airlines (VN)"
        
        public override string ToString() => DisplayName;
        
        public override bool Equals(object? obj)
        {
            if (obj is AirlineSelectDto other)
                return AirlineId == other.AirlineId;
            return false;
        }
        
        public override int GetHashCode() => AirlineId.GetHashCode();
    }
}
