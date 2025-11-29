using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace AirlineReservation_AR.src.Shared.Helper
{
    public static class SeatClassCache
    {
        private static Dictionary<int, string> _nameById;
        private static Dictionary<int, decimal> _priceMultiplierById;
        private static bool _loaded = false;

        // load 1 lần duy nhất
        public static void Initialize()
        {
            if (_loaded) return;

            using var db = DIContainer.CreateDb();
            var list = db.SeatClasses.ToList();

            _nameById = list.ToDictionary(x => x.SeatClassId, x => x.DisplayName);
            _priceMultiplierById = list.ToDictionary(x => x.SeatClassId, x => x.PriceMultiplier);

            _loaded = true;
        }

        public static string GetDisplayName(int seatClassId)
        {
            Initialize();
            return _nameById.ContainsKey(seatClassId)
                ? _nameById[seatClassId]
                : "Unknown";
        }

        public static decimal GetMultiplier(int seatClassId)
        {
            Initialize();
            return _priceMultiplierById.ContainsKey(seatClassId)
                ? _priceMultiplierById[seatClassId]
                : 1.0m;
        }

        // optional: lấy tất cả
        public static Dictionary<int, string> GetAllNames()
        {
            Initialize();
            return _nameById;
        }
    }
}
