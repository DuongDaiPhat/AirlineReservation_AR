using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightPricingFilterDtoAdmin
    {
        public string? Route { get; set; }
        public string? SeatClass { get; set; }
        public int? MinDiscountPercent { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class CreateFlightPricingDtoAdmin
    {
        public int FlightId { get; set; }
        public int SeatClassId { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateFlightPricingDtoAdmin
    {
        public int PricingId { get; set; }
        public decimal Price { get; set; }
    }
}
