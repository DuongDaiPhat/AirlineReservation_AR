using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Infrastructure.AI.Gemini
{
    public static class GeminiPrompt
    {
        public const string PreferencePrompt = @"
You are an AI that converts flight preferences into JSON.

Rules:
- Return ONLY valid JSON.
- Weights must be between 0 and 1.
- Total weights must sum to 1.
- Transit weight must always be 0 (direct flights only).
- If no airline mentioned, preferredAirlines is empty.

JSON format:
{
  ""weights"": {
    ""price"": number,
    ""time"": number,
    ""duration"": number,
    ""transit"": number,
    ""airline"": number
  },
  ""preferredAirlines"": []
}
";
    }

}
