using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirlineReservation_AR.src.Application.Interfaces.IFlightPricingServiceAdmin;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class PromotionControllerAdmin
    {
        private readonly IPromotionServiceAdmin _service;

        public PromotionControllerAdmin(IPromotionServiceAdmin service)
        {
            _service = service;
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> GetAllPromotions()
        {
            return await _service.GetAllPromotionsAsync();
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> GetPromotionById(int promotionId)
        {
            return await _service.GetPromotionByIdAsync(promotionId);
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> GetPromotionByCode(string promoCode)
        {
            return await _service.GetPromotionByCodeAsync(promoCode);
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> SearchPromotions(
            PromotionFilterDtoAdmin filter)
        {
            return await _service.SearchPromotionsAsync(filter);
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> GetActivePromotions()
        {
            var filter = new PromotionFilterDtoAdmin { IsActive = true };
            return await _service.SearchPromotionsAsync(filter);
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> SearchByKeyword(
            string keyword)
        {
            var filter = new PromotionFilterDtoAdmin { SearchTerm = keyword };
            return await _service.SearchPromotionsAsync(filter);
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> CreatePromotion(
            CreatePromotionDtoAdmin dto)
        {
            return await _service.CreatePromotionAsync(dto);
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> UpdatePromotion(
            UpdatePromotionDtoAdmin dto)
        {
            return await _service.UpdatePromotionAsync(dto);
        }

        public async Task<ServiceResponse<bool>> TogglePromotion(int promotionId)
        {
            return await _service.TogglePromotionAsync(promotionId);
        }

        public async Task<ServiceResponse<bool>> DeletePromotion(int promotionId)
        {
            return await _service.DeletePromotionAsync(promotionId);
        }

        public async Task<ServiceResponse<bool>> ValidatePromoCode(
            string promoCode,
            decimal orderAmount)
        {
            return await _service.ValidatePromoCodeAsync(promoCode, orderAmount);
        }

        public async Task<ServiceResponse<PromotionDiscountResultAdmin>> ApplyPromotion(
            string promoCode,
            decimal orderAmount,
            Guid? userId = null)
        {
            var dto = new ApplyPromotionDtoAdmin
            {
                PromoCode = promoCode,
                OrderAmount = orderAmount,
                UserId = userId
            };
            return await _service.ApplyPromotionAsync(dto);
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> GetExpiringPromotions(
            int daysAhead = 7)
        {
            var filter = new PromotionFilterDtoAdmin
            {
                IsActive = true,
                SortBy = "expiring_soon"
            };
            return await _service.SearchPromotionsAsync(filter);
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> GetMostUsedPromotions()
        {
            var filter = new PromotionFilterDtoAdmin { SortBy = "most_used" };
            return await _service.SearchPromotionsAsync(filter);
        }
    }
}
