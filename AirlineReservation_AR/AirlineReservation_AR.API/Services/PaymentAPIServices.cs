using AirlineReservation_AR.API.Interfaces;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.API.Services
{
    public class PaymentAPIServices : IPaymentAPI
    {
        private readonly AirlineReservationDbContext _db;
        public PaymentAPIServices(AirlineReservationDbContext airlineReservationDbContext)
        {
            _db = airlineReservationDbContext;
        }

        public int CreatePayment(PaymentCreateDTO dto)
        {
            string transactionId = $"{dto.BookingId}{DateTime.Now:yyyyMMddHHmmss}";
            var payment = new Payment
            {
                BookingId = dto.BookingId,
                Amount = dto.Amount,
                PaymentMethod = dto.Method,
                PaymentProvider = "MOMO",
                Currency = "VND",
                Status = "Pending",
                ProcessedAt = DateTime.Now,
                TransactionId = transactionId,
            };

            _db.Payments.Add(payment);
            _db.SaveChanges();

            _db.PaymentHistories.Add(new PaymentHistory
            {
                PaymentId = payment.PaymentId,
                Status = "Pending",
                TransactionTime = DateTime.Now,
                Note = "Waiting for user payment",
                Payment = payment,
            });

            _db.SaveChanges();

            return payment.PaymentId;
        }
    }
}
