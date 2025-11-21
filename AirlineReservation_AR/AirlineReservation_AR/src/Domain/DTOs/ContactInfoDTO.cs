using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class ContactInfoDTO
    {
        public string FirstName { get; set; }
        public string MiddleAndLastName { get; set; }

        public string PhoneCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
