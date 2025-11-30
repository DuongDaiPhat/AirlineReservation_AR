using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    public partial class UCPaidTicket : UserControl
    {
        private Booking _booking;
        private Flight _flight;
        private Ticket _ticket;
        public UCPaidTicket()
        {
            InitializeComponent();
            btnDetail.Click += BtnDetail_Click;
        }
        public void SetData(Booking booking, Flight flight, Ticket ticket)
        {
            _booking = booking;
            _flight = flight;
            _ticket = ticket;
            // Lấy thông tin thành phố + IATA
            string fromCity = flight.DepartureAirport?.City?.CityName ?? "";
            string fromCode = flight.DepartureAirport?.IataCode ?? "";

            string toCity = flight.ArrivalAirport?.City?.CityName ?? "";
            string toCode = flight.ArrivalAirport?.IataCode ?? "";

            txtFromCtyToCty.Text = $"{fromCity} ({fromCode})  ->  {toCity} ({toCode})";

            // Mã đặt chỗ
            txtBookingReferences.Text = $"BookingID: {booking.BookingReference}";

            // Ngày giờ bay
            var departDateTime = flight.FlightDate.Date + flight.DepartureTime;
            txtTakeOffTime.Text = $"{departDateTime:ddd, dd MMM yyyy - HH:mm}";

            // Airline
            txtAirline_Airport_Terminal.Text = flight.Airline?.AirlineName ?? "Airline";

            var status = ticket.Status ?? "";

            decimal? total = booking?.Price + booking?.Taxes + booking?.Fees;
            txtTotal.Text = "$" + total?.ToString("#,##0.00", CultureInfo.InvariantCulture);


            btnTicketStatus.Text = status;

            if (status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
            {
                btnTicketStatus.FillColor = Color.Crimson;
                btnTicketStatus.ForeColor = Color.White;
            }
            else if (status.Equals("Issued", StringComparison.OrdinalIgnoreCase))
            {
                btnTicketStatus.FillColor = Color.Green;
                btnTicketStatus.ForeColor = Color.White;
            }
            else if (status.Equals("Boarding", StringComparison.OrdinalIgnoreCase))
            {
                btnTicketStatus.FillColor = Color.MediumSeaGreen;
                btnTicketStatus.ForeColor = Color.White;
            }
            else
            {
                // Default
                btnTicketStatus.FillColor = Color.Silver;
                btnTicketStatus.ForeColor = Color.Black;
            }
        }

        private void BtnDetail_Click(object? sender, EventArgs e)
        {
            if (_booking == null || _flight == null || _ticket == null) return;

            using (var dlg = new Form())
            {
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.ShowInTaskbar = false;
                dlg.Text = "Manage Booking";
                dlg.Size = new Size(640, 750);

                var detailPage = new UCTicketDetail();
                detailPage.Dock = DockStyle.Fill;
                detailPage.SetData(_booking, _flight, _ticket);

                dlg.Controls.Add(detailPage);
                dlg.ShowDialog(this.FindForm());
            }
        }
    }
}
