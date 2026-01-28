using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.Entities
{
    public class PasswordResetOtp
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string OtpCode { get; set; } = null!;

        public DateTime ExpiredAt { get; set; }

        public bool IsUsed { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
