using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Domain.Exceptions;

namespace AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services
{
    public class PromotionService : IPromotionService
    {
        public bool ApplyPromotion(string promoCode, int bookingId, decimal discountAmount)
        {
            using var _db = DIContainer.CreateDb();
            try
            {

                var promotion = _db.Promotions.FirstOrDefault(p =>
                    p.PromoCode == promoCode &&
                    p.IsActive &&
                    p.ValidFrom <= DateTime.Now &&
                    p.ValidTo >= DateTime.Now);

                if (promotion == null)
                    throw new BusinessException("Promotion is invalid or expired.");

                if (promotion.UsageLimit.HasValue &&
                    promotion.UsageCount >= promotion.UsageLimit.Value)
                    throw new BusinessException("Promotion usage limit reached.");

                var bookingPromotion = new BookingPromotion
                {
                    BookingId = bookingId,
                    AppliedAt = DateTime.Now,
                    DiscountAmount = discountAmount,
                    PromotionId = promotion.PromotionId
                };
                promotion.UsageCount += 1;
                _db.BookingPromotions.Add(bookingPromotion);
                int result = _db.SaveChanges();
                
                if (result > 0)
                {
                    AuditLogService
                        .LogSimpleActionAsync(
                        DIContainer.CurrentUser?.UserId,
                        "Promotions",
                        "apply",
                        bookingId.ToString()).Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error applying promotion: " + ex.Message);

            }
        }

        //private readonly AirlineReservationDbContext _context;


        //public PromotionService(AirlineReservationDbContext context)
        //{
        //    _context = context;
        //}

        public List<PromotionDTO> GetActivePromotions()
    {
            var now = DateTime.Now;
            using var _db = DIContainer.CreateDb();
            return _db.Promotions
                .Where(p => p.IsActive == true
                            && p.ValidFrom <= now
                            && p.ValidTo >= now)
                .OrderByDescending(p => p.ValidFrom)
                .Select(p => new PromotionDTO
                {
                    PromotionID = p.PromotionId,
                    PromoCode = p.PromoCode,
                    PromoName = p.PromoName,
                    Description = p.Description,
                    ValidFrom = p.ValidFrom,
                    ValidTo = p.ValidTo,
                    PromotionType = PromotionHelper.GetPromotionType(p.PromoName)
                })
                .ToList();
        }

        public decimal getDiscountPercentage(string promoCode, int bookingId, decimal totalAmount)
        {
            using var db = DIContainer.CreateDb();
            var user = DIContainer.CurrentUser;

            var promotion = db.Promotions.FirstOrDefault(p =>
                p.PromoCode == promoCode &&
                p.IsActive &&
                p.ValidFrom <= DateTime.Now &&
                p.ValidTo >= DateTime.Now);

            if (promotion == null)
                throw new BusinessException("Promotion is invalid or expired.");

            if (promotion.UsageLimit.HasValue &&
                promotion.UsageCount >= promotion.UsageLimit.Value)
                throw new BusinessException("Promotion usage limit reached.");

            int userUsedCount = db.BookingPromotions.Count(bp =>
                bp.PromotionId == promotion.PromotionId &&
                bp.Booking.UserId == user.UserId);

            if (promotion.UserUsageLimit.HasValue &&
                userUsedCount >= promotion.UserUsageLimit.Value)
                throw new BusinessException("Promotion usage limit reached for this user.");

            if (totalAmount < promotion.MinimumAmount)
                throw new BusinessException("Booking amount does not meet promotion requirement.");

            decimal discount;

            if (promotion.DiscountType == "Percent")
            {
                discount = totalAmount * promotion.DiscountValue / 100;
                if (promotion.MaximumDiscount.HasValue)
                    discount = Math.Min(discount, promotion.MaximumDiscount.Value);
            }
            else
            {
                discount = promotion.DiscountValue;
            }

            // Log discount calculation
            AuditLogService
                .LogSimpleActionAsync(
                DIContainer.CurrentUser?.UserId,
                "Promotions",
                "calculate",
                bookingId.ToString()).Wait();

            return discount;

        }

        //public async Task<Promotion?> GetByIdAsync(int promotionId)
        //{
        //    return await _context.Promotions
        //        .FirstOrDefaultAsync(p => p.PromotionId == promotionId);
        //}

        //public async Task<Promotion?> GetByCodeAsync(string promoCode)
        //{
        //    return await _context.Promotions
        //        .FirstOrDefaultAsync(p => p.PromoCode == promoCode);
        //}

        //public async Task<IEnumerable<Promotion>> GetAllAsync()
        //{
        //    return await _context.Promotions
        //        .OrderByDescending(p => p.ValidFrom)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync(DateTime? asOf = null)
        //{
        //    var now = asOf ?? DateTime.UtcNow;

        //    return await _context.Promotions
        //        .Where(p =>
        //            p.IsActive &&
        //            p.ValidFrom <= now &&
        //            p.ValidTo >= now &&
        //            (!p.UsageLimit.HasValue || p.UsageCount < p.UsageLimit))
        //        .ToListAsync();
        //}

        //public async Task<Promotion> CreateAsync(Promotion promotion)
        //{
        //    await _context.Promotions.AddAsync(promotion);
        //    await _context.SaveChangesAsync();
        //    return promotion;
        //}

        //public async Task<bool> UpdateAsync(Promotion promotion)
        //{
        //    _context.Promotions.Update(promotion);
        //    return await _context.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> DeleteAsync(int promotionId)
        //{
        //    var promotion = await _context.Promotions.FindAsync(promotionId);
        //    if (promotion == null) return false;

        //    _context.Promotions.Remove(promotion);
        //    return await _context.SaveChangesAsync() > 0;
        //}

        //public async Task<bool> IsPromotionValidAsync(string promoCode, decimal bookingAmount, DateTime? asOf = null)
        //{
        //    var promotion = await GetByCodeAsync(promoCode);
        //    if (promotion == null) return false;

        //    var now = asOf ?? DateTime.UtcNow;

        //    if (!promotion.IsActive) return false;
        //    if (promotion.ValidFrom > now || promotion.ValidTo < now) return false;
        //    if (promotion.MinimumAmount.HasValue && bookingAmount < promotion.MinimumAmount.Value) return false;
        //    if (promotion.UsageLimit.HasValue && promotion.UsageCount >= promotion.UsageLimit.Value) return false;

        //    return true;
        //}

        //public async Task<bool> IncrementUsageAsync(int promotionId)
        //{
        //    var promotion = await _context.Promotions.FindAsync(promotionId);
        //    if (promotion == null) return false;

        //    if (promotion.UsageLimit.HasValue && promotion.UsageCount >= promotion.UsageLimit.Value)
        //        return false;

        //    promotion.UsageCount += 1;
        //    return await _context.SaveChangesAsync() > 0;
        //}
    }
}
