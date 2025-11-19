using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AirlineReservationDbContext _db;
        public UserService(AirlineReservationDbContext db)
        {
            _db = db;
        }


        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _db.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ActivateUserAsync(Guid userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(Guid userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, string? phone, string? gender, string? address)
        {
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
    }
}
