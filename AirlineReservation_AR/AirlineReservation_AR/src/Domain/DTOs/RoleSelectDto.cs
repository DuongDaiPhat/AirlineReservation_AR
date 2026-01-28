using System;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    /// <summary>
    /// DTO for role dropdown selection
    /// </summary>
    public class RoleSelectDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;  // "Admin", "Manager", "Staff", "Customer"
        public string DisplayName { get; set; } = string.Empty;  // For UI display
        
        public override string ToString() => DisplayName;
        
        public override bool Equals(object? obj)
        {
            if (obj is RoleSelectDto other)
                return RoleId == other.RoleId;
            return false;
        }
        
        public override int GetHashCode() => RoleId.GetHashCode();
    }
}
