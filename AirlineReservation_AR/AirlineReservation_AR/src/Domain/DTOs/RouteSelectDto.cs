using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    /// <summary>
    /// DTO for route dropdown selection
    /// </summary>
    public class RouteSelectDto
    {
        public string Code { get; set; } = string.Empty;  // "HAN-SGN"
        public string DisplayName { get; set; } = string.Empty;  // "Hanoi → Ho Chi Minh City"
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        
        public override string ToString() => DisplayName;
        
        public override bool Equals(object? obj)
        {
            if (obj is RouteSelectDto other)
                return Code == other.Code;
            return false;
        }
        
        public override int GetHashCode() => Code?.GetHashCode() ?? 0;
    }
}
