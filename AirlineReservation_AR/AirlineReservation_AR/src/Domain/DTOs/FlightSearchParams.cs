using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class FlightSearchParams
    {
        public string FromCode { get; set; }
        public string ToCode { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }

        public string SeatClass { get; set; }

        public int SeatClassId { get; set; }
        public bool RoundTrip { get; set; }
    }
}
