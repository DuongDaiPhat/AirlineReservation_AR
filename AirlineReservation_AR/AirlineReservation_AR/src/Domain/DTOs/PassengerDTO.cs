using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PassengerDTO
    {
        public string PassengerType { get; set; } // Adult / Child / Infant

        public string Title { get; set; } // Mr / Mrs / Miss
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Passport / CCCD
        public string? PassportNumber { get; set; }
        public string? Nationality { get; set; }
        public string? CountryIssue { get; set; }
        public DateTime? PassportExpire { get; set; }
    }
}
