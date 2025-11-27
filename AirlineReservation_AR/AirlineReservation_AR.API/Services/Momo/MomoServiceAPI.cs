using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AirlineReservation_AR.API.Services.Momo.Configurations;

namespace AirlineReservation_AR.API.Services.Momo
{
    public class MomoServiceAPI
    {
        private readonly AirlineReservationDbContext _db;
        private readonly MomoConfiguration _config;
        public MomoServiceAPI(AirlineReservationDbContext db, MomoConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public async Task<string?> CreatePaymentMomoAsync(int bookingId, long amount)
        {
          
            string requestId = Guid.NewGuid().ToString();

            var payment = await _db.Payments.FirstOrDefaultAsync(p => p.BookingId ==  bookingId);
            if (payment == null) {
                Console.WriteLine("Do not find payment with bookingId:" + bookingId);
                return null;
            }
            string orderId = payment.TransactionId;
            string rawHash =
            $"accessKey={_config.AccessKey}" +
            $"&amount={amount}" +
            $"&extraData=" +
            $"&ipnUrl={_config.IpnUrl}" +
            $"&orderId={orderId}" +
            $"&orderInfo=Thanh toan booking {bookingId}" +
            $"&partnerCode={_config.PartnerCode}" +
            $"&redirectUrl={_config.ReturnUrl}" +
            $"&requestId={requestId}" +
            $"&requestType=captureWallet";

            string signature = SignSHA256(rawHash, _config.SecretKey);

            var request = new MomoPaymentConfiguration
            {
                partnerCode = _config.PartnerCode,
                orderId = orderId,
                requestId = requestId,
                amount = amount,
                orderInfo = $"Thanh toan booking {bookingId}",
                redirectUrl = _config.ReturnUrl,
                ipnUrl = _config.IpnUrl,
                extraData = "",
                requestType = "captureWallet",
                signature = signature,
                lang = "vi"
            };

            var http = new HttpClient();
            var response = await http.PostAsync(
                _config.PaymentUrl,
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();
            var json = JsonSerializer.Deserialize<JsonElement>(content);

            return json.TryGetProperty("payUrl", out var url)
            ? url.GetString()
            : null;
        }




        private string SignSHA256(string data, string key)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }
}
