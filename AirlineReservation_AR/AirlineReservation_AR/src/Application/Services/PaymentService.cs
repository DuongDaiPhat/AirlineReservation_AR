using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AirlineReservationDbContext _db;

        public PaymentService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task<Payment> CreateAsync(
            int bookingId,
            decimal amount,
            string paymentMethod,
            string? currency = null,
            string? paymentProvider = null)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if (booking == null) throw new Exception("Booking not found.");

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Currency = currency,
                PaymentProvider = paymentProvider,
                Status = "Pending",
                ProcessedAt = null,
                CompletedAt = null,
                RefundedAmount = 0
            };

            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetByIdAsync(int paymentId)
        {
            return await _db.Payments
                .Include(p => p.Booking)
                .Include(p => p.PaymentHistories)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task<IEnumerable<Payment>> GetByBookingAsync(int bookingId)
        {
            return await _db.Payments
                .Where(p => p.BookingId == bookingId)
                .Include(p => p.PaymentHistories)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _db.Payments
                .Include(p => p.Booking)
                .OrderByDescending(p => p.PaymentId)
                .ToListAsync();
        }

        public async Task<bool> MarkProcessedAsync(int paymentId, string? transactionId = null, string? status = null)
        {
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.ProcessedAt = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(transactionId))
                payment.TransactionId = transactionId;
            if (!string.IsNullOrWhiteSpace(status))
                payment.Status = status;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkCompletedAsync(int paymentId, string? status = null)
        {
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.CompletedAt = DateTime.UtcNow;
            payment.Status = status ?? "Completed";

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RefundAsync(int paymentId, decimal refundAmount, string? status = null)
        {
            if (refundAmount <= 0) throw new Exception("Refund amount must be greater than zero.");

            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            var currentRefunded = payment.RefundedAmount ?? 0;
            if (currentRefunded + refundAmount > payment.Amount)
                throw new Exception("Refund amount exceeds original payment.");

            payment.RefundedAmount = currentRefunded + refundAmount;
            if (!string.IsNullOrWhiteSpace(status))
                payment.Status = status;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int paymentId, string status)
        {
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.Status = status;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
