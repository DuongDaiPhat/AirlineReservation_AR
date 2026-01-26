using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Domain.DTOs.AI_DTO
{
    public class FlightScoreDTO
    {
        public Flight Flight { get; set; } = null!;
        public double[] Vector { get; set; } = null!;
        public double Score { get; set; }
    }
}
