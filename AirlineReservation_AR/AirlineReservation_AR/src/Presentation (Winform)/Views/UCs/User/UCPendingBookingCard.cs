using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Application.Services;
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

        public event EventHandler? BookingCancelled;
        public UCPendingBookingCard()
        {
            InitializeComponent();
            btnCancel.Click += BtnCancel_Click;
        }

        private void UCPendingBookingCard_Load(object sender, EventArgs e)
        {

        }
        public void SetData(Booking booking, Flight? flight)
        {
            _booking = booking;
            _flight = flight;
            if (flight != null &&
                flight.DepartureAirport != null &&
                flight.ArrivalAirport != null &&
                flight.DepartureAirport.City != null &&
                flight.ArrivalAirport.City != null)
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

            btnPaymentStatus.Text = booking.Status ?? "Pending";
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
                var ok = await service.CancelBookingAsync(_booking.BookingId, UserSession.UserId);

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
            }
        }
    }
}
