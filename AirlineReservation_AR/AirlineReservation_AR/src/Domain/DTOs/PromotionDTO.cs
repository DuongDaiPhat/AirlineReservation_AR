using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PromotionDTO
    {
        public int PromotionID { get; set; }
        public string PromoCode { get; set; }
        public string PromoName { get; set; }
        public string Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        // Loại hiển thị trên UI: "Special Campaigns", "Flights", ...
        public string PromotionType { get; set; }
    }

}
