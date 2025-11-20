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

namespace MomoQR
{
    public class MomoService
    {
        public async Task CreatePaymentAsync(long amount)
        {
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();

            // Quan trọng: KHÔNG DẤU hoặc ENCODE
            string orderInfo = "WinForms thanh toan";
            // hoặc: string orderInfo = Uri.EscapeDataString("WinForms thanh toán");

            string redirectUrl = MomoConfiguration.ReturnUrl;
            string ipnUrl = MomoConfiguration.IpnUrl;

            // CHUỖI RAWHASH PHẢI EXACT THỨ TỰ MO MO API V2
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
                orderInfo = orderInfo,   // phải giống 100% rawHash
                redirectUrl = MomoConfiguration.ReturnUrl,
                ipnUrl = MomoConfiguration.IpnUrl,
                extraData = "",
                requestType = "captureWallet",
                lang = "vi",
                signature = signature
            };
            // LOG: Raw hash string for signature
            Console.WriteLine("=== MOMO DEBUG INFO ===");
            Console.WriteLine($"RawHash: {rawHash}");
            Console.WriteLine($"Signature: {signature}");
            Console.WriteLine($"OrderId: {orderId}");
            Console.WriteLine($"RequestId: {requestId}");
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine($"OrderInfo: {orderInfo}");

            var client = new HttpClient();
            var requestJson = JsonSerializer.Serialize(request);
            
            // LOG: Request payload
            Console.WriteLine($"Request JSON: {requestJson}");
            
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(MomoConfiguration.PaymentUrl, content);
            string result = await response.Content.ReadAsStringAsync();

            // LOG: Response from MoMo
            Console.WriteLine($"Response: {result}");
            Console.WriteLine("======================");

            var json = JsonSerializer.Deserialize<JsonElement>(result);

            // Check for error codes
            if (json.TryGetProperty("resultCode", out var resultCode))
            {
                int code = resultCode.GetInt32();
                Console.WriteLine($"Result Code: {code}");
                
                if (code != 0)
                {
                    string message = json.TryGetProperty("message", out var msg) ? msg.GetString() : "Unknown error";
                    MessageBox.Show($"MoMo Error Code: {code}\nMessage: {message}\n\nCheck Console for details", "MoMo Error");
                    return;
                }
            }

            // Lấy payUrl an toàn
            if (json.TryGetProperty("payUrl", out var payUrl))
            {
                Console.WriteLine($"Opening PayUrl: {payUrl.GetString()}");
                Process.Start(new ProcessStartInfo(payUrl.GetString()) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show(result, "MoMo Error - No PayUrl");
            }

            Console.WriteLine($"PARTNER={MomoConfiguration.PartnerCode}\n" +
    $"ACCESS={MomoConfiguration.AccessKey}\n" +
    $"SECRET={MomoConfiguration.SecretKey}\n" +
    $"PAYMENT_URL={MomoConfiguration.PaymentUrl}\n");
        }


        private string SignSHA256(string data, string key)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }
}
