using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Module { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }
}
