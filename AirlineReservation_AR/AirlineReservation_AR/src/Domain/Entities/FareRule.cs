using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineReservation_AR.src.Domain.Entities
{
    public class FareRule
    {
        public int FareRuleId { get; set; }

        public string FareCode { get; set; } = null!;

        public bool AllowChange { get; set; }

        public decimal? ChangeFee { get; set; }

        public int MinHoursBeforeChange { get; set; }

        public bool AllowRefund { get; set; }
        public decimal? RefundFee { get; set; }
    }
}
