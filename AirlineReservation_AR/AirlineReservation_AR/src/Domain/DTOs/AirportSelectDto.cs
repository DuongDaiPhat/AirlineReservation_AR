using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    /// <summary>
    /// DTO for airport dropdown selection
    /// </summary>
    public class AirportSelectDto
    {
        public int AirportId { get; set; }
        public string Code { get; set; } = string.Empty;  // IATA code (HAN, SGN, DAD)
        public string DisplayName { get; set; } = string.Empty;  // "Hanoi (HAN)"
        public string? CityName { get; set; }
        
        public override string ToString() => DisplayName;
        
        public override bool Equals(object? obj)
        {
            if (obj is AirportSelectDto other)
                return AirportId == other.AirportId;
            return false;
        }
        
        public override int GetHashCode() => AirportId.GetHashCode();
    }
}
