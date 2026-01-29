using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class UserContrller
    {
        private readonly IUserService _userService;
        public UserContrller(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userService.GetByIdAsync(userId);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userService.GetAllAsync();
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _userService.GetByEmailAsync(email);
        }
        public async Task<bool> ActivateUserAsync(Guid userId)
        {
            return await _userService.ActivateUserAsync(userId);
        }

        public async Task<bool> DeactivateUserAsync(Guid userId)
        {
            return await _userService.DeactivateUserAsync(userId);
        }

        public async Task<bool> UpdateProfileAsync(
            Guid userId,
            string? phone,
            string? gender,
            string? address)
        {
            return await _userService.UpdateProfileAsync(
                userId, phone, gender, address);
        }

        public async Task<(bool Success, string Message)> AddUserRoleAsync(string email, int roleId)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "Email không hợp lệ");

            var result = await _userService.AddUserRoleAsync(email, roleId);

            return result
                ? (true, "Thêm role thành công")
                : (false, "Không thể thêm — có thể user không tồn tại hoặc đã có role này");
        }
        public async Task<(bool Success, string Message)> DeleteUserAsync(Guid userId)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(userId);
                return result ? (true, "User deleted successfully.") : (false, "User not found.");
            }
            catch (Exception)
            {
                return (false, "Cannot delete user. Dependent data exists (e.g. Bookings). Please Disable instead.");
            }
        }
    }
}
