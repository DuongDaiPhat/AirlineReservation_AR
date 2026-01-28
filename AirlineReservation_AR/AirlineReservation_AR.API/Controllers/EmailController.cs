using AirlineReservation_AR.API.Interfaces;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirlineReservation_AR.API.Controllers
{
    [Route("v1/api/EmailAPI")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("booking-confirmation/{bookingId}")]
        public async Task<IActionResult> SendBookingConfirmation(int bookingId)
        {
            await _emailService.SendBookingQrAsync(bookingId);
            return Ok(new
            {
                success = true,
                message = "Email gửi thành công (nếu không có lỗi SMTP)"
            });
        }

        // 1. Request OTP
        [HttpPost("request")]
        public async Task<IActionResult> RequestCode([FromBody] RequestOtpDto dto)
        {
            await _emailService.RequestCodeAsync(dto.Email);
            return Ok(new { message = "If email exists, OTP has been sent." });
        }

        // 2. Verify OTP
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
        {
            var isValid = await _emailService.VerifyOtpAsync(dto.Email, dto.Otp);

            if (!isValid)
                return BadRequest("Invalid or expired OTP");

            return Ok("OTP is valid");
        }

        // 3. Reset Password
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await _emailService.ResetPasswordAsync(dto.Email, dto.NewPassword);
            return Ok("Password reset successfully");
        }


    }
}
