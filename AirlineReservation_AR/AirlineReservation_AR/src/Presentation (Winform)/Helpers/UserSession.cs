using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Presentation__Winform_.Helpers
{
    public static class UserSession
    {
        public static Guid UserId { get; set; }
        public static string FullName { get; set; } = null!;
        public static string Email { get; set; } = null!;
        public static string? Phone { get; set; }

        public static bool IsInitialized => UserId != Guid.Empty;

        public static void Initialize(Guid userId, string fullName, string email, string? phone)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            Phone = phone;
        }

        public static void Clear()
        {
            UserId = Guid.Empty;
            FullName = string.Empty;
            Email = string.Empty;
            Phone = null;
        }
    }
}
