using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface IBookingPromotionService
    {
        Task<BookingPromotion> ApplyPromotionAsync(int bookingId, int promotionId, decimal discountAmount);
        Task<BookingPromotion?> GetByIdAsync(int id);
        Task<IEnumerable<BookingPromotion>> GetByBookingAsync(int bookingId);
        Task<IEnumerable<BookingPromotion>> GetByPromotionAsync(int promotionId);
        Task<bool> UpdateAsync(int id, decimal? discountAmount = null);
        Task<bool> DeleteAsync(int id);
    }
}
