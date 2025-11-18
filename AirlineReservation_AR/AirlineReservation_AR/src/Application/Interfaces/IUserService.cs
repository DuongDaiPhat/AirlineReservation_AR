using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByEmailAsync(string email);

        Task<bool> ActivateUserAsync(Guid userId);
        Task<bool> DeactivateUserAsync(Guid userId);

        Task<bool> UpdateProfileAsync(Guid userId, string? phone, string? gender, string? address);
        Task<bool> AddUserRoleAsync(string userEmail, int newRoleId);
    }
}
