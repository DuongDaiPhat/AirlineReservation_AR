using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class AuthenticationController
    {
        private readonly IAuthentication _authentication;
        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        public async Task<LoginResultDTO?> LoginAsync(string email, string password)
        {
            return await _authentication.LoginAsync(email, password);
        }

        public async Task<User?> RegisterAsync(
           string fullName,
           string email,
           string password,
           string? phone = null
       )
        {
            try
            {
                return await _authentication.RegisterAsync(fullName, email, password, phone);
            }
            catch(Exception ex)
            {
                AnnouncementForm form = new AnnouncementForm();
                form.SetAnnouncement("Lỗi đăng ký", ex.Message, false, null);
                form.Show();
                return null;
            }
        }
    }
}
