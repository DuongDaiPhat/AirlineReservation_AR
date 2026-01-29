using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AirlineReservation_AR.src.AirlineReservation.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AirlineReservationDbContext _airlineReservationDbContext;
        public PaymentService(AirlineReservationDbContext airlineReservationDbContext)
        {
            _airlineReservationDbContext = airlineReservationDbContext;
        }
        public PaymentService() { }
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
            int result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                await AuditLogService
                    .LogSimpleActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.create,
                    payment.PaymentId.ToString());
            }
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

            var oldStatus = payment.Status;
            payment.ProcessedAt = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(transactionId))
                payment.TransactionId = transactionId;
            if (!string.IsNullOrWhiteSpace(status))
                payment.Status = status;

            int result = await _db.SaveChangesAsync();
            
            if (result > 0)
            {
                await AuditLogService
                    .LogActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.update,
                    payment.PaymentId.ToString(),
                    oldStatus,
                    status ?? oldStatus);
            }
            return true;
        }

        public async Task<bool> MarkCompletedAsync(int paymentId, string? status = null)
        {
            using var _db = DIContainer.CreateDb();
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            var oldStatus = payment.Status;
            payment.CompletedAt = DateTime.UtcNow;
            payment.Status = status ?? "Completed";

            int result = await _db.SaveChangesAsync();
            
            if (result > 0)
            {
                await AuditLogService
                    .LogActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.update,
                    payment.PaymentId.ToString(),
                    oldStatus,
                    payment.Status);
            }
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

            var oldRefundAmount = payment.RefundedAmount;
            payment.RefundedAmount = currentRefunded + refundAmount;
            if (!string.IsNullOrWhiteSpace(status))
                payment.Status = status;

            int result = await _db.SaveChangesAsync();
            
            if (result > 0)
            {
                await AuditLogService
                    .LogActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.update,
                    payment.PaymentId.ToString(),
                    $"Refunded: {oldRefundAmount}",
                    $"Refunded: {payment.RefundedAmount}");
            }
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int paymentId, string status)
        {
            using var _db = DIContainer.CreateDb();
            var payment = await _db.Payments.FindAsync(paymentId);
            if (payment == null) return false;

            var oldStatus = payment.Status;
            payment.Status = status;
            int result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                await AuditLogService
                    .LogActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.update,
                    payment.PaymentId.ToString(),
                    oldStatus,
                    status);
            }
            return true;
        }

        public async Task<int> CreatePaymentAsync(PaymentCreateDTO dto)
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
            int result = db.SaveChanges();

            if (result > 0)
            {
                await AuditLogService
                    .LogSimpleActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.create,
                    payment.PaymentId.ToString());
            }

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

        public void HandlePaymentStatus(int bookingId)
        {
            using var db = DIContainer.CreateDb();

            var payment = db.Payments
                .Where(p => p.BookingId == bookingId)
                .OrderByDescending(p => p.PaymentId)
                .FirstOrDefault();

            if (payment == null) return;

            if (payment.Status == "Success")
            {
                // --- UPDATE BOOKING ---
                var booking = db.Bookings.First(b => b.BookingId == bookingId);
                booking.Status = "Paid";

                // --- CREATE TICKET ---
                var ticket = new Ticket
                {
                    BookingFlightId = bookingId,
                    TicketId = Guid.NewGuid(),
                    CheckedInAt = DateTime.Now,
                    Status = "Active"
                };
                db.Tickets.Add(ticket);

                // --- UPDATE PAYMENT ---
                payment.Status = "Success";
                payment.CompletedAt = DateTime.Now;

                db.SaveChanges();
                
                // Log payment success
                AuditLogService
                    .LogSimpleActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    "success",
                    payment.PaymentId.ToString()).Wait();
            }
            else if (payment.Status == "Failed" || payment.Status == "Canceled")
            {
                var booking = db.Bookings.First(b => b.BookingId == bookingId);
                booking.Status = "Canceled";

                payment.CompletedAt = DateTime.Now;

                db.SaveChanges();
                
                // Log payment failure
                AuditLogService
                    .LogSimpleActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    "failed",
                    payment.PaymentId.ToString()).Wait();
            }
        }
        public void MarkPaymentSuccess(int bookingId, string momoTransId)
        {
            using var db = DIContainer.CreateDb();

            var payment = db.Payments.First(p => p.BookingId == bookingId);
            payment.CompletedAt = DateTime.Now;
            var oldStatus = payment.Status;
            payment.Status = "Success";

            // 2. Log history
            db.PaymentHistories.Add(new PaymentHistory
            {
                PaymentId = payment.PaymentId,
                Status = "Success",
                TransactionTime = DateTime.Now,
                Note = "Thanh toán thành công MoMo",
                Payment = payment,
            });


            // 4. Add tickets
            var bookingFlightId = db.BookingFlights
                .Where(x => x.BookingId == bookingId)
                .Select(x => x.BookingFlightId)
                .First();

            var passengers = db.Passengers.Where(p => p.BookingId == bookingId).ToList();

            foreach (var ps in passengers)
            {
                db.Tickets.Add(new Ticket
                {
                    TicketId = Guid.NewGuid(),
                    BookingFlightId = bookingFlightId,
                    PassengerId = ps.PassengerId,
                    SeatClassId = 1,
                    TicketNumber = "TKT-" + Guid.NewGuid().ToString("N")[..10],
                    Status = "Issued"
                });
            }

            int result = db.SaveChanges();
            
            if (result > 0)
            {
                AuditLogService
                    .LogActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.update,
                    payment.PaymentId.ToString(),
                    oldStatus,
                    "Success").Wait();
            }
        }

        public void MarkPaymentFailed(int bookingId, string? reason = null)
        {
            using var db = DIContainer.CreateDb();

            var payment = db.Payments.First(p => p.BookingId == bookingId);
            var oldStatus = payment.Status;
            var booking = db.Bookings.First(b => b.BookingId == bookingId);

            payment.Status = "Failed";
            payment.ProcessedAt = DateTime.Now;

            db.PaymentHistories.Add(new PaymentHistory
            {
                PaymentId = payment.PaymentId,
                Status = "Failed",
                TransactionTime = DateTime.Now,
                Note = reason ?? "Thanh toán thất bại"
            });

            booking.Status = "Canceled";

            int result = db.SaveChanges();
            
            if (result > 0)
            {
                AuditLogService
                    .LogActionAsync(
                    DIContainer.CurrentUser?.UserId,
                    TableNameAuditLog.Payments,
                    OperationAuditLog.update,
                    payment.PaymentId.ToString(),
                    oldStatus,
                    "Failed").Wait();
            }
        }
    }
}
