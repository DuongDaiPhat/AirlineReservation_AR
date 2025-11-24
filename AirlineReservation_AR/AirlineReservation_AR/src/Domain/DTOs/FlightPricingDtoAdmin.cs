using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightPricingDtoAdmin
    {
        public int PricingId { get; set; }
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public string AirlineName { get; set; } = null!;
        public string Route { get; set; } = null!;
        public string SeatClass { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int BookedSeats { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime FlightDate { get; set; }
        public string DepartureTime { get; set; } = null!;
        public string ArrivalTime { get; set; } = null!;
    }
}
