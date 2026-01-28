using AirlineReservation_AR.src.Domain.DTOs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class BookingDetailForm : Form
    {
        private readonly BookingDtoAdmin _booking;

        public BookingDetailForm(BookingDtoAdmin booking)
        {
            InitializeComponent();
            _booking = booking;
            LoadData();
        }

        private void LoadData()
        {
            if (_booking == null) return;

            pnlContent.Controls.Clear();
            int y = 0;
            int labelWidth = 140;
            int valueWidth = 350;

            // --- General ---
            AddSectionHeader("General Info", ref y);
            AddInfoRow("Booking Ref:", _booking.BookingReference, ref y, labelWidth, valueWidth);
            AddInfoRow("Date:", _booking.BookingDate.ToString("dd/MM/yyyy HH:mm"), ref y, labelWidth, valueWidth);
            AddInfoRow("Status:", _booking.Status, ref y, labelWidth, valueWidth);
            
            y += 15;

            // --- Customer ---
            AddSectionHeader("Customer Info", ref y);
            AddInfoRow("Name:", _booking.CustomerName, ref y, labelWidth, valueWidth);
            AddInfoRow("Email:", _booking.ContactEmail, ref y, labelWidth, valueWidth);
            AddInfoRow("Phone:", _booking.ContactPhone, ref y, labelWidth, valueWidth);
            AddInfoRow("Passengers:", $"{_booking.PassengerCount}", ref y, labelWidth, valueWidth);

            y += 15;

            // --- Flight ---
            if (_booking.FlightInfo != null)
            {
                AddSectionHeader("Flight Details", ref y);
                AddInfoRow("Flight No:", _booking.FlightInfo.FlightNumber, ref y, labelWidth, valueWidth);
                AddInfoRow("Route:", $"{_booking.FlightInfo.DepartureAirport} -> {_booking.FlightInfo.ArrivalAirport}", ref y, labelWidth, valueWidth);
                AddInfoRow("Departure:", $"{_booking.FlightInfo.FlightDate:dd/MM/yyyy} {_booking.FlightInfo.DepartureTime}", ref y, labelWidth, valueWidth);
                AddInfoRow("Arrival:", $"{_booking.FlightInfo.ArrivalTime}", ref y, labelWidth, valueWidth);
                AddInfoRow("Aircraft:", $"{_booking.FlightInfo.AircraftName} ({_booking.FlightInfo.AircraftType})", ref y, labelWidth, valueWidth);
                AddInfoRow("Class:", _booking.FlightInfo.SeatClass, ref y, labelWidth, valueWidth);
            }
            
            y += 15;

            // --- Payment ---
            AddSectionHeader("Payment Info", ref y);
            AddInfoRow("Total Amount:", $"{_booking.TotalAmount:N0} VND", ref y, labelWidth, valueWidth, true);
            AddInfoRow("Method:", _booking.PaymentMethod ?? "—", ref y, labelWidth, valueWidth);
            AddInfoRow("Status:", _booking.PaymentStatus, ref y, labelWidth, valueWidth);
        }

        private void AddSectionHeader(string title, ref int y)
        {
            var lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 80, 144), // PrimaryActive
                Location = new Point(0, y),
                AutoSize = true
            };
            pnlContent.Controls.Add(lbl);
            
            // Underline
            var line = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(pnlContent.Width - 20, 1),
                Location = new Point(0, y + 25)
            };
            pnlContent.Controls.Add(line);

            y += 35;
        }

        private void AddInfoRow(string label, string value, ref int y, int labelW, int valueW, bool isBold = false)
        {
            var lblKey = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.DimGray,
                Location = new Point(10, y),
                Size = new Size(labelW, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 10, isBold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isBold ? Color.Maroon : Color.Black,
                Location = new Point(10 + labelW, y),
                Size = new Size(valueW, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };

            pnlContent.Controls.Add(lblKey);
            pnlContent.Controls.Add(lblValue);

            y += 30;
        }
    }
}
