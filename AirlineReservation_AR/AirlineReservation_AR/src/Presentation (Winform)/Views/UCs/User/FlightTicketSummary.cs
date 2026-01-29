using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    public partial class FlightTicketSummary : UserControl
    {
        private Booking _booking;
        private Flight _flight;
        private Ticket _ticket;
        public FlightTicketSummary()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gán ảnh QR vào pictureBox QR
        /// </summary>
        private Bitmap GenerateQr(string bookingCode)
        {
            var payload = new { bookingCode };
            string json = JsonSerializer.Serialize(payload);

            var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(json, QRCodeGenerator.ECCLevel.Q);
            var qr = new QRCode(data);

            return qr.GetGraphic(20);
        }
        public void SetQrImage()
        {
            if (_booking == null || QR == null)
                return;

            // EmailService.GenerateQr expects a string booking code (booking reference)
            QR.Image = GenerateQr(_booking.BookingReference);
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void FlightTicketSummary_Load(object sender, EventArgs e)
        {

        }
        public void SetData(Booking booking, Flight flight, Ticket ticket)
        {
            _booking = booking;
            _flight = flight;
            _ticket = ticket;

            // Booking ID
            txtBookingId.Text = $"BookingID: {booking.BookingReference}";

            // Route: TP HCM (SGN) -> Bangkok (BKK)
            var fromCity = flight.DepartureAirport?.City?.CityName ?? flight.DepartureAirport?.IataCode;
            var toCity = flight.ArrivalAirport?.City?.CityName ?? flight.ArrivalAirport?.IataCode;
            txtFromToPlace.Text =
                $"{fromCity} ({flight.DepartureAirport?.IataCode}) -> {toCity} ({flight.ArrivalAirport?.IataCode})";

            // Hãng + hạng ghế
            txtAirline.Text = flight.Airline?.AirlineName ?? "Unknown airline";
            txtSeatClass.Text = _ticket.SeatClass?.ClassName ?? "Economy";

            // Thời gian cất cánh / hạ cánh
            var depart = flight.FlightDate.Date + flight.DepartureTime;
            var arrive = flight.FlightDate.Date + flight.ArrivalTime;

            txtTOTime.Text = depart.ToString("HH:mm");
            txtTODay.Text = depart.ToString("ddd, dd MMM");
            txtTOYear.Text = depart.ToString("yyyy");

            txtArrTime.Text = arrive.ToString("HH:mm");
            txtArrDay.Text = arrive.ToString("ddd, dd MMM");
            txtArrYear.Text = arrive.ToString("yyyy");

            //Hãng hàng không
            txtFromAirport.Text = flight.DepartureAirport?.AirportName.ToString();
            txtToAirport.Text = flight.ArrivalAirport?.AirportName.ToString();

            txtFlightPrice.Text = booking.Price.ToString("#,##0.00", CultureInfo.InvariantCulture);
            txtTax.Text = booking.Taxes?.ToString("#,##0.00", CultureInfo.InvariantCulture);
            txtFee.Text = booking.Fees?.ToString("#,##0.00", CultureInfo.InvariantCulture);

            decimal? total = booking?.Price + booking?.Taxes + booking?.Fees;
            txtTotal.Text = "$" + total?.ToString("#,##0.00", CultureInfo.InvariantCulture);


            // Thời lượng bay
            var minutes = flight.DurationMinutes ?? (int)(arrive - depart).TotalMinutes;
            txtEstimateTime.Text = $"{minutes / 60}h {minutes % 60}m";

            // ===== Passenger =====
            var p = ticket.Passenger;

            if (p != null)
            {
                // Ví dụ: "Mr (Adult) DUONG DAI PHAT"
                var type = string.IsNullOrWhiteSpace(p.PassengerType) ? "Adult" : p.PassengerType;
                var fullName = $"{p.LastName} {p.FirstName}".ToUpper().Trim();

                txtPassengerInfo.Text = $"{p.Title} ({type}) {fullName}";

                // Ví dụ: "PASSPORT: C1234567"
                txtPassport.Text = string.IsNullOrWhiteSpace(p.IdNumber)
                    ? string.Empty
                    : $"PASSPORT: {p.IdNumber}";
            }
            else
            {
                txtPassengerInfo.Text = "Passenger details not available";
                txtPassport.Text = string.Empty;
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
