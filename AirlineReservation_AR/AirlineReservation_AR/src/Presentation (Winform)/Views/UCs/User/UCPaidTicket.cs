using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


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
            if (_booking == null || _flight == null || _ticket == null)
                return;

            // Tạo user control summary
            var summary = new FlightTicketSummary();
            summary.Dock = DockStyle.Fill;
            summary.SetData(_booking, _flight, _ticket);

            // Bọc trong 1 Form làm popup
            using (var dlg = new Form())
            {
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.ShowInTaskbar = false;
                dlg.Text = "Ticket Summary";

                dlg.ClientSize = summary.Size; // 450x409 như designer
                dlg.Controls.Add(summary);

                dlg.ShowDialog(this.FindForm());
            }
        }
    }
}
