using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Interfaces;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class AuthenticationController
    {
        private readonly IAuthentication _authentication;
        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        public async Task<User?> LoginAsync(string email, string password)
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
            catch
            {
                // Quăng exception ra Form xử lý UI
                throw;
            }
        }
    }
}
