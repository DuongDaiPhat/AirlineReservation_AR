using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Services
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermission>> GetByRoleIdAsync(int roleId);
        Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(int permissionId);

        Task<bool> AssignPermissionAsync(int roleId, int permissionId);
        Task<bool> RemovePermissionAsync(int roleId, int permissionId);

        Task<bool> RoleHasPermissionAsync(int roleId, int permissionId);
    }
}
