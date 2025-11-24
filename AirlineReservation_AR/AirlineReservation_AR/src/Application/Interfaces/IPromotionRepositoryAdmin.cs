using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Interfaces
{
    public interface IPromotionRepositoryAdmin
    {
        Task<IEnumerable<Promotion>> GetAllAsync();
        Task<Promotion?> GetByIdAsync(int promotionId);
        Task<Promotion?> GetByCodeAsync(string promoCode);
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync();
        Task<IEnumerable<Promotion>> SearchAsync(string? searchTerm, bool? isActive, string? discountType);
        Task<Promotion> AddAsync(Promotion promotion);
        Task UpdateAsync(Promotion promotion);
        Task DeleteAsync(int promotionId);
        Task<bool> IsPromoCodeUniqueAsync(string promoCode, int? excludePromotionId = null);
        Task IncrementUsageCountAsync(int promotionId);
    }
}
