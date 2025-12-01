using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;
using AR_Winform.Presentation.UControls.User;
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
    public partial class FrmRescheduleSelectDate : Form
    {
        private Ticket _ticket;
        private Booking _booking;
        private RescheduleController _rescheduleController;

        private UC_FlightDate _flightDateControl;
        private DateTime _selectedDate;
        private Flight? _currentFlight;

        public bool RescheduleSucceeded { get; private set; } = false;
        public FrmRescheduleSelectDate(Ticket ticket, Booking booking)

        {
            InitializeComponent();

            _ticket = ticket;
            _booking = booking ?? throw new ArgumentNullException(nameof(booking));
            _rescheduleController = DIContainer.RescheduleConroller;

            btnChangeDate.Click += btnChangeDate_Click;
            btnSearchFlights.Click += btnSearchFlights_Click;
        }

        private void FrmRescheduleSelectDate_Load(object sender, EventArgs e)
        {
            // Lấy flight hiện tại
            _currentFlight = _ticket.BookingFlight.Flight;

            lblCurrentRoute.Text = $"{_currentFlight.DepartureAirport.CityCode} → {_currentFlight.ArrivalAirport.CityCode}";
            lblCurrentFlight.Text =
                $"{_currentFlight.FlightNumber} · {_currentFlight.FlightDate:dd/MM/yyyy} · " +
                $"{_currentFlight.DepartureTime:hh\\:mm} – {_currentFlight.ArrivalTime:hh\\:mm}";

            lblCurrentSeatClass.Text = _ticket.SeatClass.DisplayName;

            _selectedDate = _currentFlight.FlightDate;
            btnChangeDate.Text = _selectedDate.ToString("dd/MM/yyyy");

            // Tạo UC chọn ngày
            _flightDateControl = new UC_FlightDate();
            _flightDateControl.DaySelected += FlightDateControl_DaySelected;
            _flightDateControl.Dock = DockStyle.Fill;
            pnlFlightDate.Controls.Add(_flightDateControl);
            pnlFlightDate.Visible = false;
        }

        private void btnChangeDate_Click(object sender, EventArgs e)
        {
            pnlFlightDate.Visible = !pnlFlightDate.Visible;
        }

        private void FlightDateControl_DaySelected(object? sender, DateTime date)
        {
            // Không cho chọn quá khứ, UC đã xử lý rồi nhưng vẫn check thêm
            if (date < DateTime.Today)
            {
                MessageBox.Show("Không thể chọn ngày quá khứ.");
                return;
            }

            _selectedDate = date;
            btnChangeDate.Text = _selectedDate.ToString("dd/MM/yyyy");

            pnlFlightDate.Visible = false;
        }

        private async void btnSearchFlights_Click(object sender, EventArgs e)
        {
            if (_selectedDate == default)
            {
                MessageBox.Show("Hãy chọn ngày muốn đổi.");
                return;
            }

            fpnlAvailableFlight.Controls.Clear();

            // Gọi Service Reschedule để tìm chuyến
            var flights = await _rescheduleController.SearchAvailableFlightsForRescheduleAsync(
                _ticket.TicketId,
                _selectedDate
            );

            if (flights == null || flights.Count == 0)
            {
                fpnlAvailableFlight.Controls.Add(new Label()
                {
                    Text = "Không có chuyến bay phù hợp trong ngày này.",
                    AutoSize = true,
                    ForeColor = Color.DimGray
                });
                return;
            }

            foreach (var f in flights)
            {
                var card = new AvailableFlightCard(f);
                card.OnSelected += AvailableFlightSelected;
                fpnlAvailableFlight.Controls.Add(card);
            }
        }

        private async void AvailableFlightSelected(AvailableFlightDto newFlight)
        {
            // 1. Lấy báo giá
            var summary = await _rescheduleController.GetQuoteSummary(_ticket.TicketId, newFlight.FlightId);

            var confirm = MessageBox.Show(
                summary + "\n\nBạn có muốn tiếp tục đổi lịch không?",
                "Xác nhận đổi lịch",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            // 2. Gọi API đổi lịch thật
            var result = await _rescheduleController.TryRescheduleTicket(_ticket.TicketId, newFlight.FlightId);

            if (result.Contains("Successful"))
            {
                RescheduleSucceeded = true;
                MessageBox.Show(result, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
