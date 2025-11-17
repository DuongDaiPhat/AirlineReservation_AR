using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public interface INotificationService
    {
        Task<Notification> CreateAsync(
            Guid userId,
            string title,
            string message,
            string type,
            string channel,
            int? relatedBookingId = null);

        Task<Notification?> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetByUserAsync(Guid userId, bool onlyUnread = false);
        Task<IEnumerable<Notification>> GetAllAsync();

        Task<bool> MarkAsReadAsync(int notificationId);
        Task<bool> MarkAllAsReadAsync(Guid userId);

        Task<bool> UpdateAsync(
            int notificationId,
            string? title = null,
            string? message = null,
            string? type = null,
            string? channel = null);

        Task<bool> DeleteAsync(int id);
    }
}
