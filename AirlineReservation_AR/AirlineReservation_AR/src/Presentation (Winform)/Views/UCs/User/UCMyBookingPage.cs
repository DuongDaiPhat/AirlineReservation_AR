using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using Guna.UI2.WinForms;
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
    public partial class UCMyBookingPage : UserControl
    {
        private readonly BookingsService_Profile _bookingService = new BookingsService_Profile();
        private readonly FlightService _flightService = new FlightService();
        private UserDTO _user;
        private int _pendingPanelDefaultHeight;

        private System.Windows.Forms.Timer _updateTimer;
        private string _lastFingerprint = string.Empty;

        public event EventHandler CheckTransactionClick;
        public UCMyBookingPage(UserDTO user)
        {
            InitializeComponent();

            _user = user;

            this.DoubleBuffered = true;

            this.Load += UCMyBookingPage_Load;

            _pendingPanelDefaultHeight = panelPendingOrders.Height;

            fpnlIssuedTicketHolder.AutoScroll = true;
            fpnlIssuedTicketHolder.FlowDirection = FlowDirection.TopDown;
            fpnlIssuedTicketHolder.WrapContents = false;

            fpnlPendingTicketHolder.AutoScroll = true;
            fpnlPendingTicketHolder.FlowDirection = FlowDirection.TopDown;
            fpnlPendingTicketHolder.WrapContents = false;

            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 2000; // 2 giây
            _updateTimer.Tick += UpdateTimer_Tick;
        }

        private async void UCMyBookingPage_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
            _updateTimer.Start();
        }
        private async void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            // Tạm dừng timer để tránh chồng chéo nếu mạng chậm
            _updateTimer.Stop();
            try
            {
                await LoadDataAsync();
            }
            finally
            {
                // Chạy lại timer bất kể thành công hay thất bại
                if (!this.IsDisposed)
                    _updateTimer.Start();
            }
        }
        private string GetDataFingerprint(List<Booking> bookings)
        {
            if (bookings == null || !bookings.Any()) return "EMPTY";

            // Sắp xếp để đảm bảo thứ tự luôn giống nhau
            var sortedList = bookings.OrderBy(b => b.BookingId).ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var b in sortedList)
            {
                // Ghi lại: ID - Status - Số lượng vé - Trạng thái từng vé
                sb.Append($"{b.BookingId}|{b.Status}|");

                if (b.BookingFlights != null)
                {
                    foreach (var bf in b.BookingFlights)
                    {
                        if (bf.Tickets != null)
                        {
                            sb.Append($"BF{bf.BookingFlightId}:");
                            foreach (var t in bf.Tickets.OrderBy(t => t.TicketId))
                            {
                                sb.Append($"{t.TicketId}-{t.Status};");
                            }
                        }
                    }
                }
                sb.Append("#"); // Ngăn cách giữa các booking
            }
            return sb.ToString();
        }
        private async Task LoadDataAsync()
        {
            // Kiểm tra user
            if (_user.UserId == Guid.Empty)
            {
                ShowEmptyState();
                return;
            }

            // Lấy dữ liệu mới nhất từ DB
            var bookings = await _bookingService.GetBookingsByUserAsync(_user.UserId);

            // 3. Kiểm tra sự thay đổi
            string currentFingerprint = GetDataFingerprint(bookings);
            if (currentFingerprint == _lastFingerprint)
            {
                // Dữ liệu y hệt lần trước -> Không làm gì cả để tránh nháy màn hình
                return;
            }

            // Có sự thay đổi -> Cập nhật UI và lưu lại fingerprint mới
            _lastFingerprint = currentFingerprint;

            // --- BẮT ĐẦU VẼ GIAO DIỆN (Logic cũ) ---

            // SuspendLayout giúp mượt hơn khi clear/add nhiều control
            this.SuspendLayout();

            fpnlIssuedTicketHolder.Controls.Clear();
            fpnlPendingTicketHolder.Controls.Clear();

            var pendingBookings = bookings
                .Where(b => string.Equals(b.Status, "Pending", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (pendingBookings.Any())
            {
                panelPendingOrders.Visible = true;
                fpnlPendingTicketHolder.Controls.Clear();

                foreach (var b in pendingBookings)
                {
                    var bf = b.BookingFlights.FirstOrDefault();
                    Flight? flight = null;

                    if (bf != null)
                    {
                        flight = await _flightService.GetByIdWithDetailsAsync(bf.FlightId);
                    }

                    var card = new UCPendingBookingCard(_user)
                    {
                        Width = fpnlPendingTicketHolder.ClientSize.Width,
                    };

                    card.SetData(b, flight);

                    // Vì đã có Timer tự quét, ta KHÔNG cần đăng ký sự kiện card.PaymentCompleted ở đây nữa.
                    // Card con chỉ cần update DB, Timer ở đây sẽ tự phát hiện DB thay đổi sau 2s.

                    fpnlPendingTicketHolder.Controls.Add(card);
                }

                if (fpnlPendingTicketHolder.Controls.Count == 1)
                {
                    var card = fpnlPendingTicketHolder.Controls[0];
                    fpnlPendingTicketHolder.AutoScroll = false;
                    fpnlPendingTicketHolder.Height = card.Height;
                    fpnlPendingTicketHolder.MinimumSize = new Size(card.Width, card.Height + 10);
                    panelPendingOrders.Height = fpnlPendingTicketHolder.Top + card.Height;
                }
                else
                {
                    fpnlPendingTicketHolder.AutoScroll = true;
                    fpnlPendingTicketHolder.MinimumSize = new Size(0, 0);
                    fpnlPendingTicketHolder.Height = 300;
                    panelPendingOrders.Height = _pendingPanelDefaultHeight;
                }
            }
            else
            {
                panelPendingOrders.Visible = false;
            }

            // ----------------- Active Tickets -----------------
            var activeBookingFlights = bookings
                .SelectMany(b => b.BookingFlights)
                .Where(bf => bf.Tickets.Any(t =>
                    string.Equals(t.Status, "Issued", StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (activeBookingFlights.Any())
            {
                pnlNoIssuedTicket.Visible = false;
                fpnlIssuedTicketHolder.Visible = true;

                foreach (var bf in activeBookingFlights)
                {
                    var booking = bf.Booking;
                    var flight = await _flightService.GetByIdWithDetailsAsync(bf.FlightId);
                    if (flight == null) continue;

                    foreach (var ticket in bf.Tickets)
                    {
                        // Lọc chỉ hiện ticket đã Issued
                        if (!string.Equals(ticket.Status, "Issued", StringComparison.OrdinalIgnoreCase))
                            continue;

                        var ticketCard = new UCPaidTicket
                        {
                            Width = fpnlIssuedTicketHolder.ClientSize.Width,
                        };

                        ticketCard.SetData(booking, flight, ticket);
                        fpnlIssuedTicketHolder.Controls.Add(ticketCard);
                    }
                }
            }
            else
            {
                pnlNoIssuedTicket.Visible = true;
                fpnlIssuedTicketHolder.Visible = false;
            }

            this.ResumeLayout();
            this.PerformLayout();
        }



        public void LoadPendingDemo()
        {
            panelPendingOrders.Visible = true;

            fpnlPendingTicketHolder.Controls.Clear();

            for (int i = 0; i < 3; i++)
            {
                var card = new UCPendingBookingCard(_user);


                fpnlPendingTicketHolder.Controls.Add(card);
            }


        }

        public void LoadPaidDemo()
        {
            pnlNoIssuedTicket.Visible = false;

            fpnlIssuedTicketHolder.Controls.Clear();

            for (int i = 0; i < 3; i++)
            {
                var card = new UCPaidTicket();


                fpnlIssuedTicketHolder.Controls.Add(card);
            }

        }
        public void ShowEmptyState()
        {
            panelPendingOrders.Visible = false;

            fpnlIssuedTicketHolder.Visible = false;

            pnlNoIssuedTicket.Visible = true;
        }

        private void fpnlIssuedTicketHolder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCheckMyTransaction_Click(object sender, EventArgs e)
        {
            CheckTransactionClick.Invoke(this, EventArgs.Empty);
        }
    }

}
