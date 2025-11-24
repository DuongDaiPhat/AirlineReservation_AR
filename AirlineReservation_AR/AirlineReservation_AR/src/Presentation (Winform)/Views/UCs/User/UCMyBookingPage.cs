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
        }

        private async void UCMyBookingPage_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            // clear cũ
            fpnlIssuedTicketHolder.Controls.Clear();
            fpnlPendingTicketHolder.Controls.Clear();

            // nếu chưa có user (phòng trường hợp test UI)
            if (_user.UserId == Guid.Empty)
            {
                panelPendingOrders.Visible = false;
                fpnlIssuedTicketHolder.Visible = false;
                pnlNoIssuedTicket.Visible = true;
                return;
            }

            var bookings = await _bookingService.GetBookingsByUserAsync(_user.UserId);

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
                        // lấy flight kèm Airport + City
                        flight = await _flightService.GetByIdWithDetailsAsync(bf.FlightId);
                    }

                    var card = new UCPendingBookingCard(_user)
                    {
                        Width = fpnlPendingTicketHolder.ClientSize.Width,
                    };

                    card.SetData(b, flight);
                    fpnlPendingTicketHolder.Controls.Add(card);
                }

                if (fpnlPendingTicketHolder.Controls.Count == 1)
                {
                    var card = fpnlPendingTicketHolder.Controls[0];

                    // ---- Resize flowpanel cho khít 1 card ----
                    fpnlPendingTicketHolder.AutoScroll = false;
                    fpnlPendingTicketHolder.Height = card.Height;
                    fpnlPendingTicketHolder.MinimumSize = new Size(card.Width, card.Height + 10);

                    panelPendingOrders.Height =
                        fpnlPendingTicketHolder.Top + card.Height;
                }
                else
                {
                    // Nhiều card -> cho scroll bình thường
                    fpnlPendingTicketHolder.AutoScroll = true;
                    fpnlPendingTicketHolder.MinimumSize = new Size(0, 0);
                    fpnlPendingTicketHolder.Height = 300; // hoặc chiều cao default lúc design

                    panelPendingOrders.Height = _pendingPanelDefaultHeight;
                }

                panelPendingOrders.PerformLayout();
            }
            else
            {
                panelPendingOrders.Visible = false;
                // nếu muốn gọn layout hơn có thể:
                // panelPendingOrders.Height = 0;
            }

            // ----------------- Active Tickets -----------------
            // bookingflight có ticket status Issued
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

                    // load flight chi tiết để hiển thị FromCity -> ToCity
                    var flight = await _flightService.GetByIdWithDetailsAsync(bf.FlightId);
                    if (flight == null) continue;

                    foreach (var ticket in bf.Tickets)
                    {
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
                // không có vé active
                pnlNoIssuedTicket.Visible = true;
                fpnlIssuedTicketHolder.Visible = false;
            }
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
