using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    /// <summary>
    /// DTO for seat class dropdown selection
    /// </summary>
    public class SeatClassSelectDto
    {
        public int SeatClassId { get; set; }
        public string Code { get; set; } = string.Empty;  // "Economy", "Business"
        public string DisplayName { get; set; } = string.Empty;
        public decimal PriceMultiplier { get; set; }
        
        public override string ToString() => DisplayName;
        
        public override bool Equals(object? obj)
        {
            if (obj is SeatClassSelectDto other)
                return SeatClassId == other.SeatClassId;
            return false;
        }
        
        public override int GetHashCode() => SeatClassId.GetHashCode();
    }
}
