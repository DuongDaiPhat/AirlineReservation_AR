using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class VerifyOtpDto
    {
        public string Email { get; set; } = null!;
        public string Otp { get; set; } = null!;
    }

}
