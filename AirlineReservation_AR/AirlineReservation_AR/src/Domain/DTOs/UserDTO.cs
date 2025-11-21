using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email {  get; set; }

        public string Phone { get; set; }


    }
}
