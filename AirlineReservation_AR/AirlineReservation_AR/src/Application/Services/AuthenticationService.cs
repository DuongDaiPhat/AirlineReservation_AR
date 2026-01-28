using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.Exceptions;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    internal class Authentication : IAuthentication
    {

        private readonly PasswordHasher _passwordHasher;

        public Authentication(PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public async Task<User> RegisterAsync(string fullName, string email, string password, string? phone = null)
        {
            using var _db = DIContainer.CreateDb();
            // Check email đã tồn tại chưa
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
                throw new Exception("Email already exists.");
            if (await _db.Users.AnyAsync(u => u.Phone == phone))
            {
                throw new Exception("Phone number already exists");
            }
            var hashed = _passwordHasher.HashPassword(password);

            var user = new User
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
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<LoginResultDTO?> LoginAsync(string email, string password)
        {
            try
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

                return new LoginResultDTO
                {
                    User = user,
                    RoleId = roleId
                };
            }
            catch (Exception ex)
            {
                throw new BusinessException("Đăng nhập thất bại. Vui lòng thử lại sau.");

            };
        }

        public async Task<bool> ForgotPassWord(string email, string newPassword)
        {
            using var db = DIContainer.CreateDb();
            try
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (db == null)
                {
                    throw new BusinessException("Tài khoảng theo email này chưa được đăng kí trong hệ thông");

                }

                user.PasswordHash = _passwordHasher.HashPassword(newPassword);
                db.Users.Update(user);
                await db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new BusinessException("Đặt lại mật khẩu thất bại. Vui lòng thử lại sau.");
            }
        }
    }
}
