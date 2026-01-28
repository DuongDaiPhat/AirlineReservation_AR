using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    public partial class UCPendingBookingCard : UserControl
    {
        private Booking _booking;
        private Flight _flight;
        private UserDTO _user;

        public event EventHandler? BookingCancelled;
        private System.Windows.Forms.Timer _expirationTimer;
        public UCPendingBookingCard(UserDTO user)
        {
            InitializeComponent();
            btnCancel.Click += BtnCancel_Click;
            _user = user;

            btnPaymentStatus.Click += BtnPaymentStatus_Click;

            // 2. Khởi tạo Timer
            _expirationTimer = new System.Windows.Forms.Timer();
            _expirationTimer.Interval = 1000; // Cập nhật mỗi 1 giây (1000ms)
            _expirationTimer.Tick += ExpirationTimer_Tick;
        }

        private void UCPendingBookingCard_Load(object sender, EventArgs e)
        {

        }

        private async void BtnCancel_Click(object? sender, EventArgs e)
        {
            if (_booking == null)
                return;

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn hủy đặt chỗ này không?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)    
                return;

            try
            {

                var service = new BookingsService_Profile();
                var ok = await service.CancelBookingAsync(_booking.BookingId, _user.UserId);

                if (!ok)
                {
                    MessageBox.Show(
                        "Không thể hủy booking này (có thể đã được xử lý hoặc không thuộc về tài khoản của bạn).",
                        "Hủy thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật UI của chính card này (option: có thể remove luôn khỏi pending list)
                var parentFlow = this.Parent as FlowLayoutPanel;
                if (parentFlow != null)
                {
                    parentFlow.Controls.Remove(this);
                    this.Dispose(); // giải phóng luôn
                }


                // Báo cho parent biết để reload Pending + Transaction nếu muốn
                BookingCancelled?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi khi hủy booking:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Hoàn vé không thành công", "Lỗi khi hủy booking", false, null);
                announcementForm1.Show();
            }
        }
        private async void ExpirationTimer_Tick(object? sender, EventArgs e)
        {
            if (_booking == null || _booking.Status != "Pending")
            {
                _expirationTimer.Stop();
                return;
            }

            // Tính thời hạn (1 tiếng từ lúc đặt)
            var deadline = _booking.BookingDate.AddHours(1);
            var remaining = deadline - DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                // Hết giờ
                _expirationTimer.Stop();

                // Cập nhật giao diện sang EXPIRED
                UpdateUIAsExpired();

                // Cập nhật DB (gọi ngầm)
                try
                {
                    var service = new BookingsService_Profile();
                    await service.CheckBookingStatusAsync(_booking.BookingId);
                }
                catch { /* Bỏ qua lỗi kết nối ngầm để tránh crash UI */ }
            }
            else
            {
                // Cập nhật text đếm ngược. Format: hh:mm:ss cho sinh động
                // Nếu bạn chỉ muốn hh:mm thì dùng: remaining.ToString(@"hh\:mm")
                btnPaymentStatus.Text = $"Pending: Expired after {remaining.ToString(@"hh\:mm\:ss")}";
            }
        }

        // Hàm helper để update giao diện khi hết hạn
        private void UpdateUIAsExpired()
        {
            btnPaymentStatus.Text = "EXPIRED";
            btnPaymentStatus.FillColor = Color.Gray;
            btnPaymentStatus.Enabled = false;
            // Ẩn nút Cancel nếu cần thiết kế yêu cầu
        }

        public void SetData(Booking booking, Flight? flight)
        {
            _booking = booking;
            _flight = flight;

            // ... (Code hiển thị FromCity -> ToCity giữ nguyên) ...
            if (flight != null && flight.DepartureAirport?.City != null && flight.ArrivalAirport?.City != null)
            {
                string fromCity = flight.DepartureAirport.City.CityName;
                string fromCode = flight.DepartureAirport.IataCode;
                string toCity = flight.ArrivalAirport.City.CityName;
                string toCode = flight.ArrivalAirport.IataCode;
                txtFromCtyToCty.Text = $"{fromCity} ({fromCode})  ->  {toCity} ({toCode})";
            }
            else
            {
                txtFromCtyToCty.Text = "Flight Processing";
            }

            txtBookingReference.Text = $"BookingId: {booking.BookingReference}";

            // --- XỬ LÝ TRẠNG THÁI & TIMER ---
            _expirationTimer.Stop(); // Reset timer trước khi set trạng thái mới

            if (booking.Status == "Pending")
            {
                var deadline = booking.BookingDate.AddHours(1);
                if (DateTime.Now >= deadline)
                {
                    // Đã hết hạn ngay từ đầu
                    UpdateUIAsExpired();
                }
                else
                {
                    // Vẫn còn hạn -> Start Timer & Cấu hình nút thanh toán
                    btnPaymentStatus.FillColor = Color.DeepSkyBlue;
                    btnPaymentStatus.Enabled = true;
                    _expirationTimer.Start();
                    // Gọi Tick ngay lập tức để hiện giờ, không cần đợi 1s sau
                    ExpirationTimer_Tick(null, null);
                }
            }
            else if (booking.Status == "Confirmed" || booking.Status == "Complete")
            {
                btnPaymentStatus.Text = "CONFIRMED";
                btnPaymentStatus.FillColor = Color.SeaGreen;
                btnPaymentStatus.Enabled = false;
            }
            else if (booking.Status == "Cancelled")
            {
                btnPaymentStatus.Text = "CANCELLED";
                btnPaymentStatus.FillColor = Color.Crimson;
                btnPaymentStatus.Enabled = false;
            }
            else // Expired hoặc trạng thái khác
            {
                UpdateUIAsExpired();
            }
        }

        private async void BtnPaymentStatus_Click(object? sender, EventArgs e)
        {
            if (_booking == null) return;

            // 1. Kiểm tra trạng thái Booking lần cuối trước khi mở form thanh toán
            var service = new BookingsService_Profile(); // Hoặc BookingsService
            string currentStatus = await service.CheckBookingStatusAsync(_booking.BookingId);

            if (currentStatus == "Expired")
            {
                MessageBox.Show("Booking này đã hết hạn thanh toán.", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Reload lại UI để hiện nút xám
                _booking.Status = "Expired";
                SetData(_booking, _flight);
                return;
            }

            // 2. Chuẩn bị Form MomoQR
            MomoQR.MomoQR momoForm = new MomoQR.MomoQR();

            // Tính tiền
            decimal amount = CalculateTotalAmount();

            // Truyền dữ liệu sang Form Momo
            momoForm.SetPayment(_booking.BookingId, amount);

            // 3. Hiển thị Form Momo và đợi kết quả
            // ShowDialog sẽ chặn tương tác ở form cha cho đến khi form con đóng lại
            if (momoForm.ShowDialog() == DialogResult.OK)
            {
                // --- XỬ LÝ KHI THANH TOÁN THÀNH CÔNG (Sau khi MomoQR đóng) ---

                // Lúc này MomoQR đã cập nhật bảng Payment -> Completed.
                // Ta cần gọi Service để cập nhật Booking -> Confirmed và Ticket -> Issued.

                bool processSuccess = await service.ProcessPaymentSuccessAsync(_booking.BookingId);

                if (processSuccess)
                {
                    MessageBox.Show("Hệ thống đã xác nhận vé máy bay của bạn!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật trạng thái đối tượng hiện tại
                    _booking.Status = "Confirmed";

                    // Dừng đồng hồ đếm ngược
                    if (_expirationTimer != null) _expirationTimer.Stop();

                    // Cập nhật lại giao diện Card (nút xanh CONFIRMED)
                    SetData(_booking, _flight);

                    // Ẩn nút Cancel vì đã mua xong
                    btnCancel.Visible = false;
                }
                else
                {
                    // Trường hợp hiếm: Thanh toán tiền thành công nhưng update booking bị lỗi (ví dụ DB lag)
                    MessageBox.Show("Thanh toán ghi nhận thành công nhưng chưa thể xuất vé. Vui lòng liên hệ hỗ trợ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Người dùng đóng form Momo mà chưa thanh toán xong hoặc thanh toán lỗi
                // Không làm gì cả, giữ nguyên trạng thái Pending
            }
        }
        private decimal CalculateTotalAmount()
        {
            decimal total = 0;
            if (_booking != null && _booking.BookingFlights != null)
            {
                // Cách 1: Nếu Ticket đã được tạo nháp (Pending) và có giá
                foreach (var bf in _booking.BookingFlights)
                {
                    if (bf.Tickets != null)
                    {
                        total += _booking.Price;
                    }
                }

                // Cách 2: Nếu chưa có Ticket, tính theo Flight Price * Số khách (Tuỳ logic DB của bạn)
                // Ví dụ: if (total == 0) total = _booking.BookingFlights.Sum(f => f.Flight.Price) * _booking.Passengers.Count;
            }

            // Fallback nếu không tính được (để tránh lỗi khi demo)
            return total > 0 ? total : 5000000;
        }
    }
}
