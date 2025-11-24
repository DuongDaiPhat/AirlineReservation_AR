using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class PromotionDtoAdmin
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
        public bool IsExpired => DateTime.Now > ValidTo;
        public int RemainingUsage => (UsageLimit ?? 0) - UsageCount;
        public double UsagePercentage => UsageLimit.HasValue && UsageLimit > 0
            ? (double)UsageCount / UsageLimit.Value * 100
            : 0;
    }
}
