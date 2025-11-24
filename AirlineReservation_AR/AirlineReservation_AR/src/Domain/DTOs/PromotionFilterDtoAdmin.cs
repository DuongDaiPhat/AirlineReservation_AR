using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PromotionFilterDtoAdmin
    {
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; }
        public string? DiscountType { get; set; }
        public string? SortBy { get; set; } // "newest", "most_used", "highest_value"
    }
}
