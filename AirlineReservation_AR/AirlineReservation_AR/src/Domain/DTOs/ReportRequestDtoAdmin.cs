using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.DTOs
{
    public class ReportRequestDtoAdmin
    {
        public string ReportType { get; set; } = "Tổng quan";
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? AirlineCode { get; set; }
        public string? RouteFilter { get; set; }
        public int TopN { get; set; } = 5;
    }
}
