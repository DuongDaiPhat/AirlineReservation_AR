using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs.AI_DTO
{
    public class AiPreferenceResponse
    {
        public Dictionary<string, double> weights { get; set; }
        public List<string> preferredAirlines { get; set; }
    }
}
