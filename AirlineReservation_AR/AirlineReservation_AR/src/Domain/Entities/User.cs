using System;
using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? CityCode { get; set; }
        public string? Address { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public City? City { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<UserRole> AssignedRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new HashSet<AuditLog>();
    }
}
