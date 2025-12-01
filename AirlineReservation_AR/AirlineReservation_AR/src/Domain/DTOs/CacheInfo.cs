using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class CacheInfo
    {
        public bool IsLoaded { get; set; }
        public int RecordCount { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsExpired { get; set; }

        public override string ToString()
        {
            return $"Cache: {(IsLoaded ? "Loaded" : "Empty")} | " +
                   $"Records: {RecordCount} | " +
                   $"Last Update: {LastUpdateTime:HH:mm:ss} | " +
                   $"Status: {(IsExpired ? "Expired" : "Valid")}";
        }
    }
}
