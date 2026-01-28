using Microsoft.AspNetCore.Mvc;
using AirlineReservation_AR.src.Application.Services.AI_Service;
using AirlineReservation_AR.src.Domain.DTOs.AI_DTO;

namespace AirlineReservation_AR.API.Controllers
{
    [ApiController]
    [Route("v1/api/AI")]
    public class AIController : ControllerBase
    {
        private readonly GeminiPreferenceService _geminiService;

        public AIController(GeminiPreferenceService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("analyze-preference")]
        public async Task<IActionResult> AnalyzePreference([FromBody] PreferenceRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.UserText))
                {
                    return BadRequest(new { error = "User text is required" });
                }

                var userPreference = await _geminiService.FromTextAsync(request.UserText);

                return Ok(new
                {
                    success = true,
                    data = userPreference
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }


}