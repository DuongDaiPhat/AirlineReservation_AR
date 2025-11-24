using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class ApplyPromotionDtoAdmin
    {
        public string PromoCode { get; set; } = null!;
        public decimal OrderAmount { get; set; }
        public Guid? UserId { get; set; }
    }
    public class PromotionDiscountResultAdmin
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public PromotionDtoAdmin? Promotion { get; set; }
    }
}
