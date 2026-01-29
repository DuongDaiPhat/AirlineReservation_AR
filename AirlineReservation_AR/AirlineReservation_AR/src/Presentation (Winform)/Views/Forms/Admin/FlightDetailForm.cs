using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Infrastructure.DI;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class FlightDetailForm : Form
    {
        private FlightListDtoAdmin _flight;

        public FlightDetailForm(FlightListDtoAdmin flight)
        {
            InitializeComponent();
            _flight = flight;
            LoadFlightData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadFlightData()
        {
            // Title
            lblTitle.Text = $"Flight Details - {_flight.FlightCode}";

            // Clear any existing controls in content panel (if any, though in Designer usually empty)
            pnlContent.Controls.Clear();

            int y = 10;

            // 1. Flight Information
            y = AddSection("Flight Information", y);
            y = AddDetailRow(y, "Airline", _flight.Airline, "Aircraft", _flight.Aircraft);
            y = AddDetailRow(y, "Flight Number", _flight.FlightCode, "Status", _flight.Status, IsStatus: true);
            y += 20;

            // 2. Route & Schedule
            y = AddSection("Route & Schedule", y);
            string[] routeParts = _flight.Route.Split(new[] { "→" }, StringSplitOptions.RemoveEmptyEntries);
            string source = routeParts.Length > 0 ? routeParts[0].Trim() : "?";
            string dest = routeParts.Length > 1 ? routeParts[1].Trim() : "?";

            y = AddDetailRow(y, "Departure", source, "Arrival", dest);
            y = AddDetailRow(y, "Date", _flight.FlightDate.ToShortDateString(), "Duration", CalculateDuration(_flight.DepartureTime, _flight.ArrivalTime));
            y = AddDetailRow(y, "Departure Time", FormatTime(_flight.DepartureTime), "Arrival Time", FormatTime(_flight.ArrivalTime));
            y += 20;

            // 3. Pricing & Availability
            y = AddSection("Pricing & Availability", y);
            y = AddDetailRow(y, "Base Price", $"{_flight.BasePrice:N0} VND", "Total Seats", _flight.TotalSeats.ToString());
            y = AddDetailRow(y, "Available Seats", _flight.AvailableSeats.ToString(), "Booked Seats", _flight.BookedSeats.ToString());
        }

        // Helper methods to dynamically add controls to pnlContent
        // Note: Even though variables are declared in Designer.cs (which is good practice for visibility),
        // we are instantiating new 'Row' controls here dynamically because the number of rows is logical structure.
        // Doing this strictly in Designer for a "Detail View" is possible but requires absolute positioning 
        // of 20+ labels which is very verbose in generated code. 
        // This hybrid approach (Designer defines Containers, Code defines Layout) is standard for WinForms.

        private int AddSection(string title, int y)
        {
            Label lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(0, y), // Relative to pnlContent
                AutoSize = true
            };
            pnlContent.Controls.Add(lbl);
            return y + 35;
        }

        private int AddDetailRow(int y, string label1, string value1, string label2, string value2, bool IsStatus = false)
        {
            // Column 1
            AddLabelValue(label1, value1, 0, y, IsStatus && label1 == "Status");

            // Column 2 (Offset 380)
            AddLabelValue(label2, value2, 380, y, IsStatus && label2 == "Status");

            return y + 60;
        }

        private void AddLabelValue(string label, string value, int x, int y, bool isStatus)
        {
            Label lblTitle = new Label
            {
                Text = label.ToUpper(),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(x, y),
                AutoSize = true
            };
            pnlContent.Controls.Add(lblTitle);

            if (isStatus)
            {
                Guna2Chip chip = new Guna2Chip
                {
                    Text = value,
                    Location = new Point(x, y + 20),
                    FillColor = GetStatusColor(value),
                    ForeColor = Color.White,
                    AutoRoundedCorners = true,
                    Size = new Size(100, 30),
                    IsClosable = false
                };
                pnlContent.Controls.Add(chip);
            }
            else
            {
                Label lblValue = new Label
                {
                    Text = value,
                    Font = new Font("Segoe UI", 11, FontStyle.Regular),
                    ForeColor = Color.Black,
                    Location = new Point(x, y + 20),
                    AutoSize = true,
                    MaximumSize = new Size(350, 0)
                };
                pnlContent.Controls.Add(lblValue);
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status.ToLower())
            {
                case "available": return Color.FromArgb(40, 167, 69); // Green
                case "full": return Color.FromArgb(220, 53, 69); // Red
                case "cancelled": return Color.Gray;
                default: return Color.FromArgb(0, 123, 255); // Blue
            }
        }

        private string FormatTime(TimeSpan time)
        {
            return DateTime.Today.Add(time).ToString("HH:mm");
        }

        private string CalculateDuration(TimeSpan start, TimeSpan end)
        {
            var diff = end - start;
            if (diff.TotalMinutes < 0) diff = diff.Add(TimeSpan.FromDays(1)); 
            return $"{(int)diff.TotalHours}h {diff.Minutes}m";
        }
    }
}
