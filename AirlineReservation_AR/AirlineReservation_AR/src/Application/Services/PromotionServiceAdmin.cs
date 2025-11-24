using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirlineReservation_AR.src.Application.Interfaces.IFlightPricingServiceAdmin;

namespace AirlineReservation_AR.src.Application.Services
{
    public class PromotionServiceAdmin : IPromotionServiceAdmin
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public PromotionServiceAdmin(IUnitOfWorkAdmin unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> GetAllPromotionsAsync()
        {
            try
            {
                var promotions = await _unitOfWork.Promotions.GetAllAsync();
                var dtos = promotions.Select(MapToDto).ToList();
                return ServiceResponse<IEnumerable<PromotionDtoAdmin>>.SuccessResponse(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<PromotionDtoAdmin>>.ErrorResponse(
                    "Lỗi khi tải danh sách khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> GetPromotionByIdAsync(int promotionId)
        {
            try
            {
                var promotion = await _unitOfWork.Promotions.GetByIdAsync(promotionId);
                if (promotion == null)
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse("Không tìm thấy khuyến mãi");
                }

                return ServiceResponse<PromotionDtoAdmin>.SuccessResponse(MapToDto(promotion));
            }
            catch (Exception ex)
            {
                return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                    "Lỗi khi tải thông tin khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> GetPromotionByCodeAsync(string promoCode)
        {
            try
            {
                var promotion = await _unitOfWork.Promotions.GetByCodeAsync(promoCode);
                if (promotion == null)
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse("Mã khuyến mãi không tồn tại");
                }

                return ServiceResponse<PromotionDtoAdmin>.SuccessResponse(MapToDto(promotion));
            }
            catch (Exception ex)
            {
                return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                    "Lỗi khi tìm kiếm mã khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<IEnumerable<PromotionDtoAdmin>>> SearchPromotionsAsync(PromotionFilterDtoAdmin filter)
        {
            try
            {
                var promotions = await _unitOfWork.Promotions.SearchAsync(
                    filter.SearchTerm,
                    filter.IsActive,
                    filter.DiscountType);

                var dtos = promotions.Select(MapToDto).ToList();

                // Apply sorting
                dtos = filter.SortBy?.ToLower() switch
                {
                    "most_used" => dtos.OrderByDescending(p => p.UsageCount).ToList(),
                    "highest_value" => dtos.OrderByDescending(p => p.DiscountValue).ToList(),
                    _ => dtos.OrderByDescending(p => p.PromotionId).ToList() // newest
                };

                return ServiceResponse<IEnumerable<PromotionDtoAdmin>>.SuccessResponse(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<PromotionDtoAdmin>>.ErrorResponse(
                    "Lỗi khi tìm kiếm khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> CreatePromotionAsync(CreatePromotionDtoAdmin dto)
        {
            try
            {
                // Validate promo code uniqueness
                var isUnique = await _unitOfWork.Promotions.IsPromoCodeUniqueAsync(dto.PromoCode);
                if (!isUnique)
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse("Mã khuyến mãi đã tồn tại");
                }

                // Validate dates
                if (dto.ValidFrom >= dto.ValidTo)
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                        "Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                }

                // Validate discount value
                if (dto.DiscountType == "Percent" && (dto.DiscountValue <= 0 || dto.DiscountValue > 100))
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                        "Giá trị giảm giá phần trăm phải từ 1-100");
                }

                var promotion = new Promotion
                {
                    PromoCode = dto.PromoCode.ToUpper(),
                    PromoName = dto.PromoName,
                    Description = dto.Description,
                    DiscountType = dto.DiscountType,
                    DiscountValue = dto.DiscountValue,
                    MinimumAmount = dto.MinimumAmount,
                    MaximumDiscount = dto.MaximumDiscount,
                    UsageLimit = dto.UsageLimit,
                    UsageCount = 0,
                    UserUsageLimit = dto.UserUsageLimit,
                    ValidFrom = dto.ValidFrom,
                    ValidTo = dto.ValidTo,
                    IsActive = true
                };

                var created = await _unitOfWork.Promotions.AddAsync(promotion);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResponse<PromotionDtoAdmin>.SuccessResponse(
                    MapToDto(created), "Tạo mã khuyến mãi thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                    "Lỗi khi tạo mã khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<PromotionDtoAdmin>> UpdatePromotionAsync(UpdatePromotionDtoAdmin dto)
        {
            try
            {
                var promotion = await _unitOfWork.Promotions.GetByIdAsync(dto.PromotionId);
                if (promotion == null)
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse("Không tìm thấy khuyến mãi");
                }

                // Validate dates
                if (dto.ValidFrom >= dto.ValidTo)
                {
                    return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                        "Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                }

                // Update fields
                promotion.PromoName = dto.PromoName;
                promotion.Description = dto.Description;
                promotion.DiscountValue = dto.DiscountValue;
                promotion.MinimumAmount = dto.MinimumAmount;
                promotion.MaximumDiscount = dto.MaximumDiscount;
                promotion.UsageLimit = dto.UsageLimit;
                promotion.ValidFrom = dto.ValidFrom;
                promotion.ValidTo = dto.ValidTo;
                promotion.IsActive = dto.IsActive;

                await _unitOfWork.Promotions.UpdateAsync(promotion);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResponse<PromotionDtoAdmin>.SuccessResponse(
                    MapToDto(promotion), "Cập nhật mã khuyến mãi thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<PromotionDtoAdmin>.ErrorResponse(
                    "Lỗi khi cập nhật mã khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<bool>> TogglePromotionAsync(int promotionId)
        {
            try
            {
                var promotion = await _unitOfWork.Promotions.GetByIdAsync(promotionId);
                if (promotion == null)
                {
                    return ServiceResponse<bool>.ErrorResponse("Không tìm thấy khuyến mãi");
                }

                promotion.IsActive = !promotion.IsActive;
                await _unitOfWork.Promotions.UpdateAsync(promotion);
                await _unitOfWork.SaveChangesAsync();

                var message = promotion.IsActive ? "Đã kích hoạt mã khuyến mãi" : "Đã tạm dừng mã khuyến mãi";
                return ServiceResponse<bool>.SuccessResponse(true, message);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ErrorResponse(
                    "Lỗi khi thay đổi trạng thái", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<bool>> DeletePromotionAsync(int promotionId)
        {
            try
            {
                var promotion = await _unitOfWork.Promotions.GetByIdAsync(promotionId);
                if (promotion == null)
                {
                    return ServiceResponse<bool>.ErrorResponse("Không tìm thấy khuyến mãi");
                }

                // Check if promotion has been used
                if (promotion.UsageCount > 0)
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        "Không thể xóa mã khuyến mãi đã được sử dụng");
                }

                await _unitOfWork.Promotions.DeleteAsync(promotionId);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResponse<bool>.SuccessResponse(true, "Xóa mã khuyến mãi thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ErrorResponse(
                    "Lỗi khi xóa mã khuyến mãi", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<bool>> ValidatePromoCodeAsync(string promoCode, decimal orderAmount)
        {
            try
            {
                var promotion = await _unitOfWork.Promotions.GetByCodeAsync(promoCode);
                if (promotion == null)
                {
                    return ServiceResponse<bool>.ErrorResponse("Mã khuyến mãi không tồn tại");
                }

                if (!promotion.IsActive)
                {
                    return ServiceResponse<bool>.ErrorResponse("Mã khuyến mãi không còn hiệu lực");
                }

                var now = DateTime.Now;
                if (now < promotion.ValidFrom || now > promotion.ValidTo)
                {
                    return ServiceResponse<bool>.ErrorResponse("Mã khuyến mãi đã hết hạn");
                }

                if (promotion.UsageLimit.HasValue && promotion.UsageCount >= promotion.UsageLimit.Value)
                {
                    return ServiceResponse<bool>.ErrorResponse("Mã khuyến mãi đã hết lượt sử dụng");
                }

                if (promotion.MinimumAmount.HasValue && orderAmount < promotion.MinimumAmount.Value)
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        $"Đơn hàng tối thiểu {promotion.MinimumAmount:N0} ₫");
                }

                return ServiceResponse<bool>.SuccessResponse(true, "Mã khuyến mãi hợp lệ");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ErrorResponse(
                    "Lỗi khi kiểm tra mã khuyến mãi", new List<string> { ex.Message });
            }
        }
        public async Task<ServiceResponse<PromotionDiscountResultAdmin>> ApplyPromotionAsync(
            ApplyPromotionDtoAdmin dto)
        {
            try
            {
                // Validate promo code first
                var validationResult = await ValidatePromoCodeAsync(dto.PromoCode, dto.OrderAmount);
                if (!validationResult.Success)
                {
                    return ServiceResponse<PromotionDiscountResultAdmin>.ErrorResponse(
                        validationResult.Message, validationResult.Errors);
                }

                var promotion = await _unitOfWork.Promotions.GetByCodeAsync(dto.PromoCode);
                if (promotion == null)
                {
                    return ServiceResponse<PromotionDiscountResultAdmin>.ErrorResponse(
                        "Mã khuyến mãi không tồn tại");
                }

                // Calculate discount
                decimal discountAmount = 0;
                if (promotion.DiscountType == "Percent")
                {
                    discountAmount = dto.OrderAmount * (promotion.DiscountValue / 100);

                    // Apply maximum discount if set
                    if (promotion.MaximumDiscount.HasValue &&
                        discountAmount > promotion.MaximumDiscount.Value)
                    {
                        discountAmount = promotion.MaximumDiscount.Value;
                    }
                }
                else // Fixed
                {
                    discountAmount = promotion.DiscountValue;
                }

                // Ensure discount doesn't exceed order amount
                if (discountAmount > dto.OrderAmount)
                {
                    discountAmount = dto.OrderAmount;
                }

                var result = new PromotionDiscountResultAdmin
                {
                    IsValid = true,
                    Message = "Áp dụng mã khuyến mãi thành công",
                    DiscountAmount = discountAmount,
                    FinalAmount = dto.OrderAmount - discountAmount,
                    Promotion = MapToDto(promotion)
                };

                return ServiceResponse<PromotionDiscountResultAdmin>.SuccessResponse(
                    result, "Áp dụng mã khuyến mãi thành công");
            }
            catch (Exception ex)
            {
                return ServiceResponse<PromotionDiscountResultAdmin>.ErrorResponse(
                    "Lỗi khi áp dụng mã khuyến mãi", ex.Message);
            }
        }
        private PromotionDtoAdmin MapToDto(Promotion promotion)
        {
            return new PromotionDtoAdmin
            {
                PromotionId = promotion.PromotionId,
                PromoCode = promotion.PromoCode,
                PromoName = promotion.PromoName,
                Description = promotion.Description,
                DiscountType = promotion.DiscountType,
                DiscountValue = promotion.DiscountValue,
                MinimumAmount = promotion.MinimumAmount,
                MaximumDiscount = promotion.MaximumDiscount,
                UsageLimit = promotion.UsageLimit,
                UsageCount = promotion.UsageCount,
                UserUsageLimit = promotion.UserUsageLimit,
                ValidFrom = promotion.ValidFrom,
                ValidTo = promotion.ValidTo,
                IsActive = promotion.IsActive
            };
        }
    }
}