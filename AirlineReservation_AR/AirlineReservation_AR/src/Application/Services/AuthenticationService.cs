using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    internal class Authentication: IAuthentication
    {
        private readonly AirlineReservationDbContext _db;
        private readonly PasswordHasher _passwordHasher;

        public Authentication(AirlineReservationDbContext db, PasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> RegisterAsync(string fullName, string email, string password, string? phone = null)
        {
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

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            bool check = _passwordHasher.VerifyPassword(password, user.PasswordHash);

            return check ? user : null;
        }

    }
}
