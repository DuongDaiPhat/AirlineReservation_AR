using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Shared.Helper
{
    public static class BookingCodeGenerator
    {
        private static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private static readonly Random random = new Random();
        public static string GenerateBookingCode(int length = 6)
        {
            return new string(
                Enumerable.Range(0, length)
                          .Select(_ => chars[random.Next(chars.Length)])
                          .ToArray()
            );
        }
    }
}
