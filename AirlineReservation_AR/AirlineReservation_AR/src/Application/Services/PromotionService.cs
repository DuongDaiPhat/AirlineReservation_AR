using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class PromotionService : IPromotion
    {
        private readonly AirlineReservationDbContext _context;

        public PromotionService(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<Promotion?> GetByIdAsync(int promotionId)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.PromotionId == promotionId);
        }

        public async Task<Promotion?> GetByCodeAsync(string promoCode)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.PromoCode == promoCode);
        }

        public async Task<IEnumerable<Promotion>> GetAllAsync()
        {
            return await _context.Promotions
                .OrderByDescending(p => p.ValidFrom)
                .ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync(DateTime? asOf = null)
        {
            var now = asOf ?? DateTime.UtcNow;

            return await _context.Promotions
                .Where(p =>
                    p.IsActive &&
                    p.ValidFrom <= now &&
                    p.ValidTo >= now &&
                    (!p.UsageLimit.HasValue || p.UsageCount < p.UsageLimit))
                .ToListAsync();
        }

        public async Task<Promotion> CreateAsync(Promotion promotion)
        {
            await _context.Promotions.AddAsync(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }

        public async Task<bool> UpdateAsync(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int promotionId)
        {
            var promotion = await _context.Promotions.FindAsync(promotionId);
            if (promotion == null) return false;

            _context.Promotions.Remove(promotion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsPromotionValidAsync(string promoCode, decimal bookingAmount, DateTime? asOf = null)
        {
            var promotion = await GetByCodeAsync(promoCode);
            if (promotion == null) return false;

            var now = asOf ?? DateTime.UtcNow;

            if (!promotion.IsActive) return false;
            if (promotion.ValidFrom > now || promotion.ValidTo < now) return false;
            if (promotion.MinimumAmount.HasValue && bookingAmount < promotion.MinimumAmount.Value) return false;
            if (promotion.UsageLimit.HasValue && promotion.UsageCount >= promotion.UsageLimit.Value) return false;

            return true;
        }

        public async Task<bool> IncrementUsageAsync(int promotionId)
        {
            var promotion = await _context.Promotions.FindAsync(promotionId);
            if (promotion == null) return false;

            if (promotion.UsageLimit.HasValue && promotion.UsageCount >= promotion.UsageLimit.Value)
                return false;

            promotion.UsageCount += 1;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
