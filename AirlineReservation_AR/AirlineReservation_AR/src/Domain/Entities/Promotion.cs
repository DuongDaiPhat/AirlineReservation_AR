using System;
using System.Collections.Generic;

namespace AirlineReservation_AR.src.AirlineReservation.Domain.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromoCode { get; set; } = null!;
        public string PromoName { get; set; } = null!;
        public string? Description { get; set; }
        public string DiscountType { get; set; } = null!;
        public decimal DiscountValue { get; set; }
        public decimal? MinimumAmount { get; set; }
        public decimal? MaximumDiscount { get; set; }
        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; }
        public int? UserUsageLimit { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsActive { get; set; }

        public ICollection<BookingPromotion> BookingPromotions { get; set; } = new HashSet<BookingPromotion>();
    }
}
