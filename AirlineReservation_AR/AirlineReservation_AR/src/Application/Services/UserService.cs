using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class UserService : IUserService
    {
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)           // Nếu bạn muốn load luôn tên Role
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ActivateUserAsync(Guid userId)
        {
            using var _db = DIContainer.CreateDb();
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(Guid userId)
        {
            using var _db = DIContainer.CreateDb();
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, string? phone, string? gender, string? address)
        {
            using var _db = DIContainer.CreateDb();
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.Phone = phone ?? user.Phone;
            user.Gender = gender ?? user.Gender;
            user.Address = address ?? user.Address;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUserRoleAsync(string userEmail, int newRoleId)
        {
            using var _db = DIContainer.CreateDb();
            try
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                    return false;

                //  Check user đã có role này chưa
                var hasRole = await _db.UserRoles
                    .AnyAsync(ur => ur.UserId == user.UserId && ur.RoleId == newRoleId);
                if (hasRole)
                    return false;

                var userRole = new UserRole
                {
                    UserId = user.UserId,
                    RoleId = newRoleId,
                    AssignedAt = DateTime.UtcNow,
                };
                await _db.UserRoles.AddAsync(userRole);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            using var _db = DIContainer.CreateDb();
            var user = await _db.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return false;

            // Delete UserRoles first to fix FK Constraint
            if (user.UserRoles != null && user.UserRoles.Any())
            {
                _db.UserRoles.RemoveRange(user.UserRoles);
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAccountAsync(
            Guid userId,
            string fullName,
            string? phone,
            string? gender,
            string? cityCode,
            string? address)
        {
            using var _db = DIContainer.CreateDb();
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.FullName = fullName;
            user.Phone = phone;
            user.Gender = gender;
            user.CityCode = cityCode;
            user.Address = address;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
