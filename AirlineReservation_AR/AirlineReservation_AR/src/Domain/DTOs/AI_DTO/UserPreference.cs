using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs.AI_DTO
{
    public class UserPreference
    {
        // [price, time, duration, transit, airline]
        public double[] Weights { get; set; } = null!;
        public List<string> PreferredAirlines { get; set; } = new();
    }

}
