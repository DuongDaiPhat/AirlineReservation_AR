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

        public int CreatePayment(PaymentCreateDTO dto)
            => _service.CreatePayment(dto);

    }
}