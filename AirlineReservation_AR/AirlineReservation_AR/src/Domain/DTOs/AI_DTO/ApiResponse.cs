using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs.AI_DTO
{
    public class ApiResponse
    {
        public bool success { get; set; }
        public UserPreference data { get; set; }
        public string error { get; set; }
    }
}
