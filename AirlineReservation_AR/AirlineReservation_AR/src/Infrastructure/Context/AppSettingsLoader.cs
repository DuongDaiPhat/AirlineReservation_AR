using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MomoQR
{
    public class AppSettingsLoader
    {
        public static void Load()
        {
            string json = File.ReadAllText("appsettings.json");

            var root = JsonSerializer.Deserialize<JsonElement>(json);
            var momo = root.GetProperty("Momo");

            MomoConfiguration.PartnerCode = momo.GetProperty("PartnerCode").GetString();
            MomoConfiguration.ReturnUrl = momo.GetProperty("ReturnUrl").GetString();
            MomoConfiguration.IpnUrl = momo.GetProperty("IpnUrl").GetString();
            MomoConfiguration.AccessKey = momo.GetProperty("AccessKey").GetString();
            MomoConfiguration.SecretKey = momo.GetProperty("SecretKey").GetString();
            MomoConfiguration.PaymentUrl = momo.GetProperty("PaymentUrl").GetString();
        }
    }
}
