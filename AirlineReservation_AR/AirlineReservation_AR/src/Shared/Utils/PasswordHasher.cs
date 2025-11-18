using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCrypt.Net;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Shared.Utils
{
    public class PasswordHasher
    {
        // Mã hóa mật khẩu
        public string HashPassword(string password)
        {
            // Work factor mặc định = 10, có thể tăng lên 12-14 nếu cần bảo mật cao hơn
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Kiểm tra mật khẩu nhập vào với hash đã lưu
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
