using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IAuthentication
    {
        Task<LoginResultDTO?> LoginAsync(string email, string password);

        Task<User> RegisterAsync(string fullName, string email, string password, string? phone = null);
    }
}
