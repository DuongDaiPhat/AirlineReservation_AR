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
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MomoQR
{
    public partial class MomoQR : Form
    {
        private event Action _paymentCompleted;
        private int _bookingId;
        private decimal _amount;
        private readonly PaymentController _controller;
        private System.Windows.Forms.Timer paymentCheckTimer;
        public MomoQR()
        {
            InitializeComponent();
            _controller = DIContainer.paymentController;
            paymentCheckTimer = new System.Windows.Forms.Timer();
            paymentCheckTimer.Interval = 2000;
            paymentCheckTimer.Tick += PaymentCheckTimer_Tick;
        }
        public void SetPayment(int bookingId, decimal amount)
        {
            _bookingId = bookingId;
            _amount = amount;
            txtAmount.Text = amount.ToString("N0");
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
            var client = new HttpClient();

            var dto = new
            {
                BookingId = _bookingId,
                Amount = (long)_amount,
                Method = "MoMo",
            };

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
            lblWalletBalance.Text = $"Total amount: {_amount:N0} đ";

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

                MessageBox.Show("Thanh toán thành công! Vé đã được phát hành.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (payment.Status == "Failed" || payment.Status == "Canceled")
            {
                paymentCheckTimer.Stop();
                _controller.MarkFailed(_bookingId, "MoMo trả về thất bại");

                MessageBox.Show("Thanh toán thất bại hoặc bị huỷ!");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
