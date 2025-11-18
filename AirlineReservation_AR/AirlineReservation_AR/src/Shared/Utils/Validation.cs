using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Shared.Utils
{
    internal class Validation
    {
        // Kiểm tra input chỉ chứa số nguyên không âm
        public bool IsNonNegativeInteger(string input)
        {
            if (!int.TryParse(input, out int result) || result < 0)
            {
                MessageBox.Show("Vui lòng nhập số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Kiểm tra mật khẩu theo quy tắc: 10-20 ký tự, ít nhất 1 chữ hoa, 1 số, 1 ký tự đặc biệt
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{10,20}$";
            if (!Regex.IsMatch(password, pattern))
            {
                MessageBox.Show("Mật khẩu phải 10–20 ký tự, có ít nhất 1 chữ hoa, 1 số, 1 ký tự đặc biệt.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // Kiểm tra số điện thoại Việt Nam: 10 số bắt đầu bằng 0 hoặc +84
        public bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^(0\d{9}|\+84\d{9})$";
            if (!Regex.IsMatch(phone ?? "", pattern))
            {
                MessageBox.Show("Số điện thoại phải 10 số bắt đầu bằng 0 hoặc dạng +84xxxxxxxxx.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Kiểm tra ngày sinh phải trước ngày hiện tại
        public bool IsValidBirthDate(DateOnly dob)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if (dob >= today)
            {
                MessageBox.Show("Ngày sinh phải trước ngày hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Kiểm tra email: phải đúng định dạng Gmail (đuôi @gmail.com).
        public bool IsValidGoogleEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            if (!Regex.IsMatch(email ?? "", pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Email phải đúng định dạng và dùng @gmail.com.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
