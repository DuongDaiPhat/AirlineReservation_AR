using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using AirlineReservation_AR.src.Domain.DTOs.AI_DTO;
using AirlineReservation_AR.src.Infrastructure.AI.Gemini;

namespace AirlineReservation_AR.src.Application.Services.AI_Service
{
    public class GeminiPreferenceService
    {
        private readonly HttpClient _http = new HttpClient();
        private readonly string _apiKey;

        // ✅ Constructor nhận IConfiguration
        public GeminiPreferenceService(IConfiguration configuration)
        {
            _apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException("Gemini API key is not configured in appsettings.json");
        }

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
            new
            {
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite:generateContent?key={_apiKey}"
            );

            request.Content = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            File.WriteAllText("gemini_raw.json", json);


            using (var errorCheck = JsonDocument.Parse(json))
            {
                if (errorCheck.RootElement.TryGetProperty("error", out var error))
                {
                    var errorMsg = error.TryGetProperty("message", out var msg)
                        ? msg.GetString()
                        : "Unknown error";
                    var errorCode = error.TryGetProperty("code", out var code)
                        ? code.GetInt32()
                        : 0;

                    throw new Exception($"Gemini API Error ({errorCode}): {errorMsg}");
                }
            }

            var rawText = ExtractGeminiText(json);


            var cleanJson = CleanGeminiJson(rawText);

            AiPreferenceResponse ai;
            try
            {
                ai = JsonSerializer.Deserialize<AiPreferenceResponse>(
                    cleanJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Gemini returned invalid JSON:\n" + cleanJson, ex);
            }


            return BuildUserPreference(ai);
        }


        private string CleanGeminiJson(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                throw new Exception("Gemini returned empty response");

            var cleaned = raw.Trim();

            // Remove markdown if exists
            cleaned = cleaned.Replace("```json", "")
                             .Replace("```", "")
                             .Trim();

            // Ensure JSON starts correctly
            var firstBrace = cleaned.IndexOf('{');
            var lastBrace = cleaned.LastIndexOf('}');

            if (firstBrace == -1 || lastBrace == -1 || lastBrace <= firstBrace)
                throw new Exception("Gemini response is not valid JSON:\n" + raw);

            return cleaned.Substring(firstBrace, lastBrace - firstBrace + 1);
        }


        private string ExtractGeminiText(string json)
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("candidates", out var candidates) ||
                candidates.ValueKind != JsonValueKind.Array ||
                candidates.GetArrayLength() == 0)
            {
                throw new Exception("Gemini response missing candidates");
            }

            var candidate = candidates[0];

            if (!candidate.TryGetProperty("content", out var content))
                throw new Exception("Gemini response missing content");

            if (!content.TryGetProperty("parts", out var parts) ||
                parts.ValueKind != JsonValueKind.Array ||
                parts.GetArrayLength() == 0)
            {
                throw new Exception("Gemini response missing parts");
            }

            if (!parts[0].TryGetProperty("text", out var textElement))
                throw new Exception("Gemini response missing text");

            return textElement.GetString();
        }

        private UserPreference BuildUserPreference(AiPreferenceResponse ai)
        {
            if (ai == null || ai.weights == null)
                throw new Exception("AI preference data is empty");

            var weights = new[]
            {
                ai.weights.GetValueOrDefault("price"),
                ai.weights.GetValueOrDefault("time"),
                ai.weights.GetValueOrDefault("duration"),
                ai.weights.GetValueOrDefault("transit"),
                ai.weights.GetValueOrDefault("airline")
            };

            // Normalize weights
            var sum = weights.Sum();
            if (sum <= 0)
                throw new Exception("Invalid AI weights");

            for (int i = 0; i < weights.Length; i++)
                weights[i] /= sum;

            return new UserPreference
            {
                Weights = weights,
                PreferredAirlines = ai.preferredAirlines ?? new List<string>()
            };
        }
    }
}