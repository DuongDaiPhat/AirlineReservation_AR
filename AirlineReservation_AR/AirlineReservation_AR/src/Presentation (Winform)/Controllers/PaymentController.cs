using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Controllers
{
    public class PaymentController
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        public async Task<int> CreatePayment(PaymentCreateDTO dto)
        {
            return await _service.CreatePaymentAsync(dto);
        }


        public void MarkSuccess(int bookingId, string momoTransId)
            => _service.MarkPaymentSuccess(bookingId, momoTransId);

        public void MarkFailed(int bookingId, string reason)
            => _service.MarkPaymentFailed(bookingId, reason);
    }
}