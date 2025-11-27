using AirlineReservation_AR.API.Interfaces;
using AirlineReservation_AR.API.Services.Momo;
using AirlineReservation_AR.src.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlineReservation_AR.API.Controllers
{
    [Route("v1/api/PaymentAPI")]
    [ApiController]
    public class PaymentControllerAPI : ControllerBase
    {
        private readonly IPaymentAPI _paymentService;
        private readonly MomoServiceAPI _momoService;

        public PaymentControllerAPI(IPaymentAPI paymentService, MomoServiceAPI momoService)
        {
            _paymentService = paymentService;
            _momoService = momoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateDTO dto)
        {
            int paymentId = _paymentService.CreatePayment(dto);

            string? payUrl = await _momoService.CreatePaymentMomoAsync(dto.BookingId, (long)dto.Amount);

            if (payUrl == null)
                return StatusCode(500, "Failed to create MoMo payment");

            return Ok(new
            {
                paymentId,
                payUrl
            });
        }
    }
}
