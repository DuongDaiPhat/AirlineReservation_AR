using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Application.Services.AI_Service
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using AirlineReservation_AR.src.Domain.DTOs.AI_DTO;
    using AirlineReservation_AR.src.Infrastructure.AI.Gemini;

    public class GeminiPreferenceService
    {
        private readonly HttpClient _http = new HttpClient();
        private const string API_KEY = "AIzaSyBgDc1L4IyDmQnNM6isUNuekdCaX9bh5e0";

        public async Task<UserPreference> FromTextAsync(string userText)
        {
            var prompt = $@"
            {GeminiPrompt.PreferencePrompt}

            User preference:
            ""{userText}""
            ";

            var body = new
            {
                contents = new[]
                {
                new {
                    parts = new[] {
                        new { text = prompt }
                    }
                }
            }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={API_KEY}"
            );

            request.Content = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            // Lấy text Gemini trả về
            var text = JsonDocument.Parse(json)
                .RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            var ai = JsonSerializer.Deserialize<AiPreferenceResponse>(text);

            return BuildUserPreference(ai);
        }

        private UserPreference BuildUserPreference(AiPreferenceResponse ai)
        {
            var weights = new[]
            {
            ai.weights.GetValueOrDefault("price"),
            ai.weights.GetValueOrDefault("time"),
            ai.weights.GetValueOrDefault("duration"),
            ai.weights.GetValueOrDefault("transit"),
            ai.weights.GetValueOrDefault("airline")
        };

            // Safety: normalize
            var sum = weights.Sum();
            if (sum != 1 && sum > 0)
            {
                for (int i = 0; i < weights.Length; i++)
                    weights[i] /= sum;
            }

            return new UserPreference
            {
                Weights = weights,
                PreferredAirlines = ai.preferredAirlines ?? new List<string>()
            };
        }
    }

}
