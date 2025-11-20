using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class AirlineFilterDTO
    {
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string LogoUrl { get; set; }
    }
}
