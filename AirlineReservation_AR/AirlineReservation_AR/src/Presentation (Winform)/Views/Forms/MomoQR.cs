using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Domain.Exceptions;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MomoQR
{
    public partial class MomoQR : Form
    {
        private event Action _paymentCompleted;
        private int _bookingId;
        private decimal _amount = 0, _dicount = 0;
        private string _promoCode = "";
        private readonly PaymentController _controller;
        private readonly PromotionController _promotionController;
        private System.Windows.Forms.Timer paymentCheckTimer;
        private LoadingForm _loadingForm;
        public MomoQR()
        {
            InitializeComponent();
            _controller = DIContainer.paymentController;
            _promotionController = DIContainer.PromotionController;
            paymentCheckTimer = new System.Windows.Forms.Timer();
            paymentCheckTimer.Interval = 2000;
            paymentCheckTimer.Tick += PaymentCheckTimer_Tick;
        }
        public void SetPayment(int bookingId, decimal amount)
        {
            _bookingId = bookingId;
            _amount = amount;         
            txtAmount.Text = $"{amount.ToString("N0")} đ";
            txtDiscount.Text = "0 đ";
            txtTotal.Text = $"{amount.ToString("N0")} đ";
        }


        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private async void payButton_Click(object sender, EventArgs e)
        {
            _loadingForm = new LoadingForm();
            _loadingForm.Show();
            _loadingForm.BringToFront();
            var client = new HttpClient();
            try
            {
                var dto = new
                {
                    BookingId = _bookingId,
                    Amount = (long)_amount - _dicount,
                    Method = "MoMo",
                };
                var result = _promotionController.ApplyPromotion(_promoCode, _bookingId, _dicount);
                
                var response = await client.PostAsync(
                    "http://localhost:5080/v1/api/PaymentAPI/create",
                    new StringContent(
                        JsonSerializer.Serialize(dto),
                        Encoding.UTF8,
                        "application/json"
                    )
                );

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Không thể tạo Payment!\nStatus: {response.StatusCode}\nLỗi: {error}");
                    return;
                }
                string json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                string? payUrl = null;

                if (doc.RootElement.TryGetProperty("payUrl", out var payUrlProp))
                    payUrl = payUrlProp.GetString();


                if (string.IsNullOrEmpty(payUrl))
                {
                    MessageBox.Show("API không trả về payUrl. Thanh toán không thể tiếp tục!");
                    return;
                }

                Process.Start(new ProcessStartInfo(payUrl) { UseShellExecute = true });
                paymentCheckTimer.Start();
                payButton.Enabled = false;


            }
            catch(BusinessException ex)
            {
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Lỗi thanh toán", ex.Message, false, null);
                announcementForm.Show();
                return;
            }
            catch (Exception ex)
            {
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Lỗi thanh toán", ex.Message, false, null);
                announcementForm.Show();
                return;
            }
        }


        private void payButton_MouseEnter(object sender, EventArgs e)
        {
            payButton.BackColor = Color.FromArgb(200, 35, 51);
        }

        private void payButton_MouseLeave(object sender, EventArgs e)
        {
            payButton.BackColor = Color.FromArgb(220, 53, 69);
        }

        private void MomoQR_Load(object sender, EventArgs e)
        {


        }
        private void PaymentCheckTimer_Tick(object sender, EventArgs e)
        {
            using var db = DIContainer.CreateDb();

            var payment = db.Payments
                .Where(p => p.BookingId == _bookingId)
                .OrderByDescending(p => p.PaymentId)
                .FirstOrDefault();

            if (payment == null) return;

            if (payment.Status == "Completed")
            {
                paymentCheckTimer.Stop();
                _controller.MarkSuccess(_bookingId, payment.TransactionId);
                
               _ =CallSendEmailApiAsync(_bookingId);
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement(
                    "Thanh toán thành công",
                    "Bạn đã thanh toán thành công bằng MoMo!",
                    true,
                    null);
                announcementForm.Show();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (payment.Status == "Failed" || payment.Status == "Canceled")
            {
                paymentCheckTimer.Stop();
                _controller.MarkFailed(_bookingId, "MoMo trả về thất bại");
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement(
                    "Thanh toán thất bại",
                    "Thanh toán MoMo của bạn đã thất bại hoặc bị hủy.",
                    false,
                    null);
                announcementForm.Show();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        public async Task CallSendEmailApiAsync(int bookingId)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:5001/");

            var response = await client.PostAsync(
                $"v1/api/EmailAPI/booking-confirmation/{bookingId}",
                null
            );

            // Optional: check status
            if (!response.IsSuccessStatusCode)
            {
                // log nhẹ, KHÔNG báo user
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ApplyDiscount_Click(object sender, EventArgs e)
        {
            _loadingForm = new LoadingForm();
            _loadingForm.Show();
            _loadingForm.BringToFront();

            _promoCode = txtPromoCode.Text.Trim();
            try
            {
                decimal discount = _promotionController.getDiscountPercentage(_promoCode, _bookingId, _amount);
                if (discount > 0)
                {
                    _dicount = discount;
                    txtTotal.Text = $"{(_amount - _dicount).ToString("N0")} đ";
                    txtDiscount.Text = $"{_dicount.ToString("N0")} đ";
                }
                else
                {
                    _dicount = 0;
                }

            }
            catch (BusinessException ex)
            {
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Lỗi áp dụng mã giảm giá", ex.Message, false, null);
                announcementForm.Show();
                return;
            }
            catch (Exception ex)
            {
                CloseLoading();
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement("Lỗi áp dụng mã giảm giá", ex.Message, false, null);
                announcementForm.Show();
                return;
            }
            CloseLoading();
        }

        private void CloseLoading()
        {
            if (_loadingForm != null && !_loadingForm.IsDisposed)
            {
                _loadingForm.Invoke(new Action(() =>
                {
                    _loadingForm.Close();
                }));
            }
        }
    }
}
