using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomoQR
{
    public class MomoConfiguration
    {
        public string PartnerCode { get; set; }
        public string ReturnUrl { get; set; }
        public string PaymentUrl { get; set; }
        public string IpnUrl { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

}
