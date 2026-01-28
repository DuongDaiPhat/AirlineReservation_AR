using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    internal class Authentication: IAuthentication
    {

        private readonly PasswordHasher _passwordHasher;

        public Authentication(PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public async Task<AirlineReservation.Domain.Entities.User> RegisterAsync(string fullName, string email, string password, string? phone = null)
        {
            using var _db = DIContainer.CreateDb();
            // Check email đã tồn tại chưa
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
                throw new Exception("Email used");
            if (await _db.Users.AnyAsync(u => u.Phone == phone))
            {
                throw new Exception("Phone number used");
            }
            var hashed = _passwordHasher.HashPassword(password);

            var user = new AirlineReservation.Domain.Entities.User
            {
                UserId = Guid.NewGuid(),
                FullName = fullName,
                Email = email,
                Phone = phone,
                PasswordHash = hashed,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsVerified = false
            };

            _db.Users.Add(user);
            int result = await _db.SaveChangesAsync();

            if(result > 0)
            {
                Console.WriteLine($"[Auth] User {email} registered successfully with ID {user.UserId}");
                Console.WriteLine($"[Auth] Calling LogSimpleAction...");
                
                // Log audit sau khi registration hoàn tất
                // Dùng phiên bản sync để tránh xung đột transaction
                await AuditLogService.LogSimpleActionAsync(
                    user.UserId,
                    TableNameAuditLog.Users,
                    OperationAuditLog.register,
                    user.UserId.ToString());
                
                Console.WriteLine($"[Auth] LogSimpleAction completed");
            }
            else
            {
                Console.WriteLine($"[Auth] Registration failed - SaveChanges returned {result}");
            }

            return user;
        }

        public async Task<LoginResultDTO?> LoginAsync(string email, string password)
        {
            using var _db = DIContainer.CreateDb();
            // Tìm user theo email
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            // Kiểm tra mật khẩu
            bool check = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            if (!check)
                return null;

            // Lấy RoleID từ bảng UserRoles
            var roleId = await _db.UserRoles
                .Where(ur => ur.UserId == user.UserId)
                .OrderByDescending(ur => ur.AssignedAt)
                .Select(ur => ur.RoleId)
                .FirstOrDefaultAsync();

            if (roleId == 0)
                roleId = 3; // nếu chưa gán role thì mặc định là user thường

            Console.WriteLine($"[Auth] User {email} logged in successfully");
            Console.WriteLine($"[Auth] Calling LogSimpleAction for login...");

            // Log login audit
            // Dùng phiên bản sync để tránh xung đột transaction
            AuditLogService.LogSimpleAction(
                user.UserId,
                TableNameAuditLog.Users,
                OperationAuditLog.login,
                user.UserId.ToString());

            Console.WriteLine($"[Auth] LogSimpleAction for login completed");

            return new LoginResultDTO
            {
                User = user,
                RoleId = roleId
            };
        }

    }
}
