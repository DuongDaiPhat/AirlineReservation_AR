using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services
{
    public class PromotionRepositoryAdmin : IPromotionRepositoryAdmin
    {
        private readonly AirlineReservationDbContext _context;

        public PromotionRepositoryAdmin(AirlineReservationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion>> GetAllAsync()
        {
            return await _context.Promotions
                .OrderByDescending(p => p.PromotionId)
                .ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(int promotionId)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.PromotionId == promotionId);
        }

        public async Task<Promotion?> GetByCodeAsync(string promoCode)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.PromoCode == promoCode.ToUpper());
        }

        public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
        {
            var now = DateTime.Now;
            return await _context.Promotions
                .Where(p => p.IsActive &&
                           p.ValidFrom <= now &&
                           p.ValidTo >= now)
                .ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> SearchAsync(
            string? searchTerm,
            bool? isActive,
            string? discountType)
        {
            var query = _context.Promotions.AsQueryable();

            // Search by code or name
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(p =>
                    p.PromoCode.ToLower().Contains(searchTerm) ||
                    p.PromoName.ToLower().Contains(searchTerm));
            }

            // Filter by status
            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive.Value);
            }

            // Filter by type
            if (!string.IsNullOrEmpty(discountType) && discountType != "Tất cả")
            {
                var type = discountType == "Phần trăm" ? "Percent" : "Fixed";
                query = query.Where(p => p.DiscountType == type);
            }

            return await query.OrderByDescending(p => p.PromotionId).ToListAsync();
        }

        public async Task<Promotion> AddAsync(Promotion promotion)
        {
            await _context.Promotions.AddAsync(promotion);
            return promotion;
        }

        public async Task UpdateAsync(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int promotionId)
        {
            var promotion = await _context.Promotions.FindAsync(promotionId);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
            }
        }

        public async Task<bool> IsPromoCodeUniqueAsync(string promoCode, int? excludePromotionId = null)
        {
            var query = _context.Promotions
                .Where(p => p.PromoCode == promoCode.ToUpper());

            if (excludePromotionId.HasValue)
            {
                query = query.Where(p => p.PromotionId != excludePromotionId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task IncrementUsageCountAsync(int promotionId)
        {
            var promotion = await _context.Promotions.FindAsync(promotionId);
            if (promotion != null)
            {
                promotion.UsageCount++;
                _context.Promotions.Update(promotion);
            }
        }
    }
}
