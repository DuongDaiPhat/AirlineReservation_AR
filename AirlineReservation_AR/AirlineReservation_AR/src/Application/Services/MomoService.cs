using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Infrastructure.DI;

namespace MomoQR
{
    public class MomoService
    {


        public async Task<string?> CreatePaymentAsync(int bookingId, long amount)
        {
            // ⭐ FIXED: Đổi từ Task → Task<string?> để trả về payUrl
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();

            using (var db = DIContainer.CreateDb())
            {
                var booking = db.Bookings.First(b => b.BookingId == bookingId);
                booking.Status = "Pending";
                db.SaveChanges();

                var payment = db.Payments.FirstOrDefault(p => p.BookingId == bookingId);
                if (payment != null)
                {
                    payment.TransactionId = orderId;   // ⭐ FIXED: cần transactionId để IPN đối chiếu
                    payment.Status = "Processing";
                    db.SaveChanges();
                }
            }

            // KHÔNG DẤU
            string orderInfo = "WinForms thanh toan booking " + bookingId;

            string redirectUrl = MomoConfiguration.ReturnUrl;
            string ipnUrl = MomoConfiguration.IpnUrl;

            string rawHash =
                $"accessKey={MomoConfiguration.AccessKey}" +
                $"&amount={amount}" +
                $"&extraData=" +
                $"&ipnUrl={MomoConfiguration.IpnUrl}" +
                $"&orderId={orderId}" +
                $"&orderInfo={orderInfo}" +
                $"&partnerCode={MomoConfiguration.PartnerCode}" +
                $"&redirectUrl={MomoConfiguration.ReturnUrl}" +
                $"&requestId={requestId}" +
                $"&requestType=captureWallet";

            string signature = SignSHA256(rawHash, MomoConfiguration.SecretKey);

            var request = new MomoPaymentConfiguration
            {
                partnerCode = MomoConfiguration.PartnerCode,
                partnerName = "MoMo Test",
                storeId = "TestStore",
                requestId = requestId,
                amount = amount,
                orderId = orderId,
                orderInfo = orderInfo,
                redirectUrl = MomoConfiguration.ReturnUrl,
                ipnUrl = MomoConfiguration.IpnUrl,
                extraData = "",
                requestType = "captureWallet",
                lang = "vi",
                signature = signature
            };

            // Logging
            Console.WriteLine("=== MOMO DEBUG INFO ===");
            Console.WriteLine($"RawHash: {rawHash}");
            Console.WriteLine($"Signature: {signature}");
            Console.WriteLine($"OrderId: {orderId}");
            Console.WriteLine($"RequestId: {requestId}");
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine($"OrderInfo: {orderInfo}");

            var client = new HttpClient();
            var requestJson = JsonSerializer.Serialize(request);
            Console.WriteLine($"Request JSON: {requestJson}");

            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(MomoConfiguration.PaymentUrl, content);
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response: {result}");
            Console.WriteLine("======================");

            var json = JsonSerializer.Deserialize<JsonElement>(result);

            // Check error
            if (json.TryGetProperty("resultCode", out var resultCode))
            {
                int code = resultCode.GetInt32();

                if (code != 0)
                {
                    string message = json.TryGetProperty("message", out var msg)
                                    ? msg.GetString() : "Unknown error";

                    MessageBox.Show($"MoMo Error Code: {code}\nMessage: {message}", "MoMo Error");
                    return null;  // ⭐ FIXED: phải trả về null khi lỗi
                }
            }

            // ⭐ FIXED: TRẢ PAYURL RA CHO FORM
            if (json.TryGetProperty("payUrl", out var payUrl))
            {
                string url = payUrl.GetString();
                Console.WriteLine($"Opening PayUrl: {url}");

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                return url;   // ⭐ FIXED: TRẢ PAYURL VỀ
            }
            else
            {
                MessageBox.Show(result, "MoMo Error - No PayUrl");
                return null;  // ⭐ FIXED
            }
        }



        private string SignSHA256(string data, string key)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }
}
