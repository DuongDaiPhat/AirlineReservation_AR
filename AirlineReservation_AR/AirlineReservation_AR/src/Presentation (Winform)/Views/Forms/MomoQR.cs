using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;

namespace MomoQR
{
    public partial class MomoQR : Form
    {
        private int _bookingId;
        private decimal _amount;
        private readonly PaymentController _controller;
        private System.Windows.Forms.Timer paymentCheckTimer;
        public MomoQR()
        {
            InitializeComponent();
            _controller = DIContainer.paymentController;
        }
        public void SetPayment(int bookingId, decimal amount)
        {
            _bookingId = bookingId;
            _amount = amount;
            txtAmount.Text = amount.ToString("N0");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
            // 1. Tạo Payment trong DB trước
            var paymentId = _controller.CreatePayment(new PaymentCreateDTO
            {
                BookingId = _bookingId,
                Amount = _amount,
                Method = "MoMo"
            });

            MessageBox.Show("Đang tạo yêu cầu thanh toán...");

            // 2. Gửi sang MoMo
            var momo = new MomoService();
            string? payUrl = await momo.CreatePaymentAsync(_bookingId, (long)_amount);

            if (payUrl == null)
            {
                MessageBox.Show("Không thể tạo thanh toán MoMo!");
                return;
            }

            // 3. Mở trang thanh toán MoMo
            Process.Start(new ProcessStartInfo(payUrl) { UseShellExecute = true });

            // ⭐⭐ 4. BẮT ĐẦU TIMER Ở ĐÂY — KHÔNG ĐƯỢC ĐÓNG FORM ⭐⭐
            paymentCheckTimer.Start();
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
            paymentCheckTimer = new System.Windows.Forms.Timer();
            paymentCheckTimer.Interval = 2000;
            paymentCheckTimer.Tick += PaymentCheckTimer_Tick;

        }

        private void PaymentCheckTimer_Tick(object sender, EventArgs e)
        {
            using (var db = DIContainer.CreateDb())
            {
                var payment = db.Payments
                    .Where(p => p.BookingId == _bookingId)
                    .OrderByDescending(p => p.PaymentId)
                    .FirstOrDefault();

                if (payment == null) return;

                if (payment.Status == "Success")
                {
                    paymentCheckTimer.Stop();
                    MessageBox.Show("Thanh toán thành công!");
                    this.Close();
                }
                else if (payment.Status == "Failed" || payment.Status == "Canceled")
                {
                    paymentCheckTimer.Stop();
                    MessageBox.Show("Thanh toán thất bại hoặc bị huỷ!");
                    this.Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
