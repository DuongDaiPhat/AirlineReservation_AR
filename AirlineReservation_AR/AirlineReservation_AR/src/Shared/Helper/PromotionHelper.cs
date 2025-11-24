using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Shared.Helper
{
    public static class PromotionHelper
    {
        public static string GetPromotionType(string promoName)
        {
            if (string.IsNullOrWhiteSpace(promoName))
                return "Others";

            var firstWord = promoName.Split(' ')[0].ToUpperInvariant();

            return firstWord switch
            {
                "CAMPAIGN" => "Special Campaigns",
                "DISCOUNT" => "Flights",
                _ => "Others"
            };
        }
    }

}
