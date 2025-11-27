using AirlineReservation_AR.API.Interfaces;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;

namespace AirlineReservation_AR.API.Services
{
    public class PaymentCallbackService : IPaymentCallbackService
    {
        private readonly AirlineReservationDbContext _db;

        public PaymentCallbackService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public string UpdatePaymentStatus(string orderId, int resultCode, string message)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.TransactionId == orderId);
            if (payment == null)
                return "NOT_FOUND";

            if (resultCode == 0)
            {
                payment.Status = "Completed";

                _db.PaymentHistories.Add(new PaymentHistory
                {
                    PaymentId = payment.PaymentId,
                    Status = "Confirmed",
                    TransactionTime = DateTime.Now,
                    Note = "MoMo payment completed"
                });
                var booking = _db.Bookings.FirstOrDefault(b => b.BookingId == payment.BookingId);
                if(booking != null)
                    booking.Status = "Confirmed";
                else
                {
                    Console.WriteLine("Do not find booking with booking id:" + payment.BookingId);
                    return "NOT_FOUND";
                }
            }
            else
            {
                payment.Status = "Failed";

                _db.PaymentHistories.Add(new PaymentHistory
                {
                    PaymentId = payment.PaymentId,
                    Status = "Canceled",
                    TransactionTime = DateTime.Now,
                    Note = "MoMo failed: " + message
                });
            }

            _db.SaveChanges();
            return payment.Status;
        }
    }
}
