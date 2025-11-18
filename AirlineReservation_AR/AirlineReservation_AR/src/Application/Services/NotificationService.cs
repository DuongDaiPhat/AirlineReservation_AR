using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AirlineReservationDbContext _db;

        public NotificationService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<Notification> CreateAsync(
            Guid userId,
            string title,
            string message,
            string type,
            string channel,
            int? relatedBookingId = null)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) throw new Exception("User not found.");

            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                Channel = channel,
                RelatedBookingId = relatedBookingId,
                IsRead = false,
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _db.Notifications
                .Include(n => n.User)
                .Include(n => n.RelatedBooking)
                .FirstOrDefaultAsync(n => n.NotificationId == id);
        }

        public async Task<IEnumerable<Notification>> GetByUserAsync(Guid userId, bool onlyUnread = false)
        {
            var query = _db.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt);

            if (onlyUnread)
                query = query.Where(n => !n.IsRead);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _db.Notifications
                .Include(n => n.User)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var noti = await _db.Notifications.FindAsync(notificationId);
            if (noti == null) return false;

            noti.IsRead = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _db.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            if (!notifications.Any()) return true;

            foreach (var n in notifications)
                n.IsRead = true;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(
            int notificationId,
            string? title = null,
            string? message = null,
            string? type = null,
            string? channel = null)
        {
            var noti = await _db.Notifications.FindAsync(notificationId);
            if (noti == null) return false;

            if (title != null) noti.Title = title;
            if (message != null) noti.Message = message;
            if (type != null) noti.Type = type;
            if (channel != null) noti.Channel = channel;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var noti = await _db.Notifications.FindAsync(id);
            if (noti == null) return false;

            _db.Notifications.Remove(noti);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
