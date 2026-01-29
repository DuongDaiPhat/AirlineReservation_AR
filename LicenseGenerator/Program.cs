using System;
using System.Security.Cryptography;
using System.Text;

namespace LicenseGenerator
{
    class Program
    {
        private const string LICENSE_SECRET = "AR2024-SECRET-KEY";
        
        static void Main(string[] args)
        {
            Console.WriteLine("=== Airline Reservation License Generator ===\n");
            
            // Generate 5 license keys
            for (int i = 0; i < 5; i++)
            {
                var key = GenerateLicenseKey();
                Console.WriteLine($"  {key}");
            }
            
            Console.WriteLine("\n=== Copy any key above to activate the app ===");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static string GenerateLicenseKey()
        {
            var random = new Random();
            var part1 = random.Next(1000, 9999).ToString();
            var part2 = random.Next(1000, 9999).ToString();
            var baseKey = $"AR-{part1}-{part2}";
            var checksum = GenerateChecksum(baseKey);
            return $"{baseKey}-{checksum}";
        }
        
        static string GenerateChecksum(string input)
        {
            var combined = input + LICENSE_SECRET;
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(combined));
            return BitConverter.ToString(hash).Replace("-", "").Substring(0, 4);
        }
    }
}
