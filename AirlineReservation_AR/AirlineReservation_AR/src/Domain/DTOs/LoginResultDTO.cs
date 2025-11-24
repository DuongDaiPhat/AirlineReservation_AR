using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class LoginResultDTO
    {
        public User User { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
