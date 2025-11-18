using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly AirlineReservationDbContext _db;

        public RoleService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        // ============= CREATE =============
        public async Task<Role> CreateAsync(string roleName, string? description = null, bool isActive = true)
        {
            var exists = await _db.Roles.AnyAsync(r => r.RoleName == roleName);
            if (exists)
                throw new Exception("Role with the same name already exists.");

            var role = new Role
            {
                RoleName = roleName,
                Description = description,
                IsActive = isActive
            };

            _db.Roles.Add(role);
            await _db.SaveChangesAsync();

            return role;
        }

        // ============= GET BY ID =============
        public async Task<Role?> GetByIdAsync(int roleId)
        {
            return await _db.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }

        // ============= GET BY NAME =============
        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _db.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        // ============= GET ALL =============
        public async Task<IEnumerable<Role>> GetAllAsync(bool includeInactive = false)
        {
            IQueryable<Role> query = _db.Roles;

            if (!includeInactive)
            {
                query = query.Where(r => r.IsActive);
            }

            return await query
                .OrderBy(r => r.RoleName)
                .ToListAsync();
        }

        // ============= UPDATE =============
        public async Task<bool> UpdateAsync(
            int roleId,
            string? roleName = null,
            string? description = null,
            bool? isActive = null)
        {
            var role = await _db.Roles.FindAsync(roleId);
            if (role == null) return false;

            if (roleName != null && roleName != role.RoleName)
            {
                var exists = await _db.Roles
                    .AnyAsync(r => r.RoleName == roleName && r.RoleId != roleId);

                if (exists)
                    throw new Exception("Another role with the same name already exists.");

                role.RoleName = roleName;
            }

            if (description != null)
                role.Description = description;

            if (isActive.HasValue)
                role.IsActive = isActive.Value;

            await _db.SaveChangesAsync();
            return true;
        }

        // ============= SET ACTIVE / INACTIVE =============
        public async Task<bool> SetActiveAsync(int roleId, bool isActive)
        {
            var role = await _db.Roles.FindAsync(roleId);
            if (role == null) return false;

            role.IsActive = isActive;
            await _db.SaveChangesAsync();

            return true;
        }

        // ============= ASSIGN PERMISSION =============
        public async Task<bool> AssignPermissionAsync(int roleId, int permissionId)
        {
            var role = await _db.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
            if (role == null) return false;

            var permission = await _db.Permissions
                .FirstOrDefaultAsync(p => p.PermissionId == permissionId);
            if (permission == null) return false;

            var exists = role.RolePermissions
                .Any(rp => rp.PermissionId == permissionId);
            if (exists) return true; // đã có rồi thì xem như success

            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            _db.RolePermissions.Add(rolePermission);
            await _db.SaveChangesAsync();

            return true;
        }

        // ============= REMOVE PERMISSION =============
        public async Task<bool> RemovePermissionAsync(int roleId, int permissionId)
        {
            var rolePermission = await _db.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission == null) return false;

            _db.RolePermissions.Remove(rolePermission);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}
