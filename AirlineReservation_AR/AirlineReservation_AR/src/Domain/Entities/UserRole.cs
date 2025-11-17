using System;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime AssignedAt { get; set; }
        public Guid? AssignedBy { get; set; }
        public string? Note { get; set; }

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public User? AssignedByUser { get; set; }
    }
}
