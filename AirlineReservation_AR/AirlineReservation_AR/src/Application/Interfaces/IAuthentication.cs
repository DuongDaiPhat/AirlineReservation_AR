using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IAuthentication
    {
        Task<User?> LoginAsync(string email, string password);

        Task<User> RegisterAsync(string fullName, string email, string password, string? phone = null);
    }
}
