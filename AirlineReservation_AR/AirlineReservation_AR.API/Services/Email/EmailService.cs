using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using AirlineReservation_AR.API.Interfaces;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using QRCoder;
using AirlineReservation_AR.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using System.Text;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;

namespace AirlineReservation_AR.API.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly AirlineReservationDbContext _db;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public EmailService(AirlineReservationDbContext db)
        {
            _db = db;
        }

        public async Task  RequestCodeAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            // Không leak thông tin
            if (user == null)
                return;

            // Invalidate OTP cũ
            var oldOtps = _db.PasswordResetOtps
                .Where(x => x.UserId == user.UserId && !x.IsUsed);

            foreach (var otp in oldOtps)
                otp.IsUsed = true;

            var otpCode = GenerateOtp();
            var hashedOtp = Hash(otpCode);

            var resetOtp = new PasswordResetOtp
            {
                UserId = user.UserId,
                OtpCode = hashedOtp,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            _db.PasswordResetOtps.Add(resetOtp);
            await _db.SaveChangesAsync();
            
            await SendEmailAsync(
                user.Email,
                "Reset Password - OTP Code",
                $"Your OTP code is: {otpCode}\nThis code is valid for 5 minutes."
            );
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",      // Gmail SMTP
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    "6451071030@st.utc2.edu.vn",   // EMAIL GỬI
                    "cbbr ghvb zocr mnbk"       // APP PASSWORD (không phải mật khẩu Gmail)
                )
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("6451071030@st.utc2.edu.vn", "Airline Reservation"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                throw new Exception("User not found");

            var otpRecord = await _db.PasswordResetOtps
                .Where(x =>
                    x.UserId == user.UserId &&
                    !x.IsUsed &&
                    x.ExpiredAt > DateTime.UtcNow)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (otpRecord == null)
                throw new Exception("Invalid or expired OTP");

            user.PasswordHash = _passwordHasher.HashPassword(newPassword);

            otpRecord.IsUsed = true;

            await _db.SaveChangesAsync();
        }

        public Task SendBookingQrAsync(int bookingId)
        {
            try
            {
                // 1. Lấy booking + passenger
                var booking = _db.Bookings
                    .Where(b => b.BookingId == bookingId)
                    .Select(b => new
                    {
                        b.BookingId,
                        b.BookingReference,
                        b.ContactEmail
                    })
                    .FirstOrDefault();

                if (booking == null)
                    return Task.CompletedTask;

                // 2. Tạo QR
                var qrImage = GenerateQr(booking.BookingReference);

                // 3. Convert QR -> MemoryStream
                using var ms = new MemoryStream();
                qrImage.Save(ms, ImageFormat.Png);
                ms.Position = 0;

                // 4. Gửi mail
                SendMail(
                    booking.ContactEmail,
                    booking.BookingReference,
                    ms
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SendBookingQrAsync: " + ex.Message);
            }

            return Task.CompletedTask;
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return false;

            var hashedOtp = Hash(otp);

            var otpRecord = await _db.PasswordResetOtps
                .Where(x =>
                    x.UserId == user.UserId &&
                    !x.IsUsed &&
                    x.ExpiredAt > DateTime.UtcNow)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (otpRecord == null)
                return false;

            return otpRecord.OtpCode == hashedOtp;
        }

        private Bitmap GenerateQr(string bookingCode)
        {
            var payload = new { bookingCode };
            string json = JsonSerializer.Serialize(payload);

            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(json, QRCodeGenerator.ECCLevel.Q);
            var qr = new QRCode(data);

            return qr.GetGraphic(20);
        }

        private void SendMail(string to, string bookingCode, Stream qrStream)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress("yourmail@gmail.com");
            mail.To.Add(to);
            mail.Subject = "Xác nhận thanh toán & QR Check-in";
            mail.Body = $@"
            Xin chào,

            Thanh toán của bạn đã được xác nhận thành công.
            Mã booking: {bookingCode}

            Vui lòng xuất trình QR đính kèm khi check-in.
            ";

            mail.Attachments.Add(
                new Attachment(qrStream, "booking_qr.png", "image/png")
            );

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    "6451071030@st.utc2.edu.vn",
                    "cbbr ghvb zocr mnbk"
                ),
                EnableSsl = true
            };

            smtp.Send(mail);
        }

        private string GenerateOtp()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        }

        private string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }


}
