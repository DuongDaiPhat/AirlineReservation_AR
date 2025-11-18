using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly AirlineReservationDbContext _context;

        public RolePermissionService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolePermission>> GetByRoleIdAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(int permissionId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Include(rp => rp.Role)
                .ToListAsync();
        }

        public async Task<bool> AssignPermissionAsync(int roleId, int permissionId)
        {
            // Nếu đã tồn tại => bỏ qua
            var exists = await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (exists) return false;

            var newRP = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            await _context.RolePermissions.AddAsync(newRP);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemovePermissionAsync(int roleId, int permissionId)
        {
            var rp = await _context.RolePermissions
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);

            if (rp == null) return false;

            _context.RolePermissions.Remove(rp);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RoleHasPermissionAsync(int roleId, int permissionId)
        {
            return await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }
    }
}
