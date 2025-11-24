using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class PaymentService : IPaymentService
    {

        public async Task<Payment> CreateAsync(
            int bookingId,
            decimal amount,
            string paymentMethod,
            string? currency = null,
            string? paymentProvider = null)
        {
            using var _db = DIContainer.CreateDb();
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
            using var _db = DIContainer.CreateDb();
            return await _db.Payments
                .Include(p => p.Booking)
                .Include(p => p.PaymentHistories)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task<IEnumerable<Payment>> GetByBookingAsync(int bookingId)
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Payments
                .Where(p => p.BookingId == bookingId)
                .Include(p => p.PaymentHistories)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            using var _db = DIContainer.CreateDb();
            return await _db.Payments
                .Include(p => p.Booking)
                .OrderByDescending(p => p.PaymentId)
                .ToListAsync();
        }

        public async Task<bool> MarkProcessedAsync(int paymentId, string? transactionId = null, string? status = null)
        {
            using var _db = DIContainer.CreateDb();
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
            using var _db = DIContainer.CreateDb();
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.CompletedAt = DateTime.UtcNow;
            payment.Status = status ?? "Completed";

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RefundAsync(int paymentId, decimal refundAmount, string? status = null)
        {
            using var _db = DIContainer.CreateDb();
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
            using var _db = DIContainer.CreateDb();
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            payment.Status = status;
            await _db.SaveChangesAsync();
            return true;
        }

        public int CreatePayment(PaymentCreateDTO dto)
        {
            using var db = DIContainer.CreateDb();

            var payment = new Payment
            {
                BookingId = dto.BookingId,
                Amount = dto.Amount,
                PaymentMethod = dto.Method,
                PaymentProvider = "MOMO",
                Currency = "VND",
                Status = "Pending",
                ProcessedAt = DateTime.Now
            };

            db.Payments.Add(payment);
            db.SaveChanges();

            db.PaymentHistories.Add(new PaymentHistory
            {
                PaymentId = payment.PaymentId,
                Status = "Pending",
                TransactionTime = DateTime.Now,
                Note = "Waiting for user payment"
            });

            db.SaveChanges();

            return payment.PaymentId;
        }

        //public void HandlePaymentStatus(int bookingId)
        //{
        //    using var db = DIContainer.CreateDb();

        //    var payment = db.Payments
        //        .Where(p => p.BookingId == bookingId)
        //        .OrderByDescending(p => p.PaymentId)
        //        .FirstOrDefault();

        //    if (payment == null) return;

        //    if (payment.Status == "Success")
        //    {
        //        // --- UPDATE BOOKING ---
        //        var booking = db.Bookings.First(b => b.BookingId == bookingId);
        //        booking.Status = "Paid";

        //        // --- CREATE TICKET ---
        //        var ticket = new Ticket
        //        {
        //            BookingFlightId = bookingId,
        //            TicketCode = "TCK-" + DateTime.Now.Ticks,
        //            IssuedAt = DateTime.Now,
        //            Status = "Active"
        //        };
        //        db.Tickets.Add(ticket);

        //        // --- UPDATE PAYMENT ---
        //        payment.Status = "Success";
        //        payment.UpdatedAt = DateTime.Now;

        //        db.SaveChanges();
        //    }
        //    else if (payment.Status == "Failed" || payment.Status == "Canceled")
        //    {
        //        var booking = db.Bookings.First(b => b.BookingId == bookingId);
        //        booking.Status = "Canceled";

        //        payment.UpdatedAt = DateTime.Now;

        //        db.SaveChanges();
        //    }
        //}
    }
}
