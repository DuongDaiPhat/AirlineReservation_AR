using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class BookingCreateDTO
    {
        public Guid UserId { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string? SpecialRequest { get; set; }

        public int FlightId { get; set; }
        public string TripType { get; set; } = "OneWay";

        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalFee { get; set; }
        public List<PassengerWithServicesDTO> Passengers { get; set; }
    }
}
