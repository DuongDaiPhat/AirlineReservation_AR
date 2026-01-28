using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
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
        // Check input contains only non-negative integers
        public bool IsNonNegativeInteger(string input)
        {
            if (!int.TryParse(input, out int result) || result < 0)
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Input Error", "Please enter a non-negative integer", false, null);
                form.Show();
                form.BringToFront();
                return false;
            }
            return true;
        }

        // Check password according to rules: 10-20 characters, at least 1 uppercase, 1 number, 1 special character
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Input Error", "Password cannot be empty.", false, null);
                form.Show();
                form.BringToFront();
                return false;
            }

            string pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{10,20}$";
            if (!Regex.IsMatch(password, pattern))
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Invalid password format", "Requirements: 10-20 characters, 1 special character, 1 uppercase letter and 1 number", false, null);
                form.Show();
                form.BringToFront();
                return false;
            }

            return true;
        }

        // Check Vietnam phone number: 10 digits starting with 0 or +84
        public bool IsValidPhoneNumber(string phone)
        {
            string pattern = @"^(0\d{9}|\+84\d{9})$";
            if (!Regex.IsMatch(phone ?? "", pattern))
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Input Error", "Invalid phone number", false, null);
                form.Show();
                form.BringToFront();
                return false;
            }
            return true;
        }

        // Check date of birth must be before today
        public bool IsValidBirthDate(DateOnly dob)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if (dob >= today)
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Input Error", "Invalid date of birth", false, null);
                form.Show();
                form.BringToFront();
                return false;
            }
            return true;
        }

        // Check email: must be correct Gmail format (ending @gmail.com).
        public bool IsValidGoogleEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            if (!Regex.IsMatch(email ?? "", pattern, RegexOptions.IgnoreCase))
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Input Error", "Email must be in correct format", false, null);
                form.Show();
                form.BringToFront();
                return false;
            }
            return true;
        }
    }
}
