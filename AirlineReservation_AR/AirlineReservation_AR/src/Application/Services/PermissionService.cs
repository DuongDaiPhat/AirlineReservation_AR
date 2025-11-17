using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AirlineReservationDbContext _db;

        public PermissionService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<Permission> CreateAsync(string name, string? description = null, string? module = null)
        {
            var exists = await _db.Permissions.AnyAsync(x => x.PermissionName == name);
            if (exists) throw new System.Exception("Permission already exists.");

            var p = new Permission
            {
                PermissionName = name,
                Description = description,
                Module = module
            };

            _db.Permissions.Add(p);
            await _db.SaveChangesAsync();
            return p;
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _db.Permissions
                .Include(p => p.RolePermissions)
                .FirstOrDefaultAsync(p => p.PermissionId == id);
        }

        public async Task<Permission?> GetByNameAsync(string name)
        {
            return await _db.Permissions
                .Include(p => p.RolePermissions)
                .FirstOrDefaultAsync(p => p.PermissionName == name);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _db.Permissions
                .OrderBy(p => p.PermissionName)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, string? name = null, string? description = null, string? module = null)
        {
            var p = await _db.Permissions.FindAsync(id);
            if (p == null) return false;

            if (name != null)
            {
                var exists = await _db.Permissions.AnyAsync(x => x.PermissionName == name && x.PermissionId != id);
                if (exists) throw new System.Exception("Permission name already exists.");
                p.PermissionName = name;
            }

            if (description != null) p.Description = description;
            if (module != null) p.Module = module;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _db.Permissions.FindAsync(id);
            if (p == null) return false;

            _db.Permissions.Remove(p);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
