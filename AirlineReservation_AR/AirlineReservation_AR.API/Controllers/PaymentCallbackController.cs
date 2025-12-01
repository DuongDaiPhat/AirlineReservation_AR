using AirlineReservation_AR.API.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlineReservation_AR.API.Controllers
{
    [ApiController]
    [Route("v1/api/PaymentCallBack")]
    public class PaymentCallbackController : ControllerBase
    {
        private readonly IPaymentCallbackService _callbackService;

        public PaymentCallbackController(IPaymentCallbackService callbackService)
        {
            _callbackService = callbackService;
        }

        [HttpPost("momo-callback")]
        public IActionResult MomoCallback([FromBody] JsonElement data)
        {
            string orderId = data.GetProperty("orderId").GetString();
            int resultCode = data.GetProperty("resultCode").GetInt32();
            string message = data.GetProperty("message").GetString();

            var status = _callbackService.UpdatePaymentStatus(orderId, resultCode, message);

            if (status == "NOT_FOUND")
                return BadRequest("Payment not found");

            return Ok(new { status });
        }
    }
}
