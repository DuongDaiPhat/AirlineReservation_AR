using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirlineReservation_AR.src.Application.Interfaces.IFlightPricingServiceAdmin;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IPromotionServiceAdmin
    {
        Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> GetAllPromotionsAsync();
        Task<ServiceResponse<PromotionDtoAdmin>> GetPromotionByIdAsync(int promotionId);
        Task<ServiceResponse<PromotionDtoAdmin>> GetPromotionByCodeAsync(string promoCode);
        Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> SearchPromotionsAsync(PromotionFilterDtoAdmin filter);
        Task<ServiceResponse<PromotionDtoAdmin>> CreatePromotionAsync(CreatePromotionDtoAdmin dto);
        Task<ServiceResponse<PromotionDtoAdmin>> UpdatePromotionAsync(UpdatePromotionDtoAdmin dto);
        Task<ServiceResponse<bool>> TogglePromotionAsync(int promotionId);
        Task<ServiceResponse<bool>> DeletePromotionAsync(int promotionId);
        Task<ServiceResponse<bool>> ValidatePromoCodeAsync(string promoCode, decimal orderAmount);
        Task<ServiceResponse<PromotionDiscountResultAdmin>> ApplyPromotionAsync(ApplyPromotionDtoAdmin dto);
    }
}
