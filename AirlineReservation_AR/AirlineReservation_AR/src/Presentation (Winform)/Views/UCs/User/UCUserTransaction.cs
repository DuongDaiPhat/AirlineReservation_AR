using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{

    public partial class UCUserTransaction : UserControl
    {
        private readonly BookingServices _bookingService = new BookingServices();
        public UCUserTransaction()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Load += UCUserTransaction_Load;

            // FlowPanel hiển thị các transaction
            fpnlTransactionHolder.AutoScroll = true;
            fpnlTransactionHolder.FlowDirection = FlowDirection.TopDown;
            fpnlTransactionHolder.WrapContents = false;
        }

        private async void UCUserTransaction_Load(object? sender, EventArgs e)
        {
            await LoadTransactionsAsync();
        }

        public async Task ReloadAsync()
        {
            await LoadTransactionsAsync();
        }

        private async Task LoadTransactionsAsync()
        {
            // clear cũ
            fpnlTransactionHolder.Controls.Clear();

            // Nếu chưa có user (trường hợp test UI)
            if (UserSession.UserId == Guid.Empty)
            {
                fpnlTransactionHolder.Visible = false;
                pnlNoTransaction.Visible = true;
                return;
            }

            // Lấy toàn bộ booking của user từ DB
            var bookings = await _bookingService.GetBookingsByUserAsync(UserSession.UserId);

            // Lọc tất cả booking KHÔNG phải Pending
            var txBookings = bookings
                .Where(b => !string.Equals(b.Status, "Pending", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            if (!txBookings.Any())
            {
                // Không có transaction nào
                fpnlTransactionHolder.Visible = false;
                pnlNoTransaction.Visible = true;
                return;
            }

            // Có dữ liệu
            pnlNoTransaction.Visible = false;
            fpnlTransactionHolder.Visible = true;

            foreach (var booking in txBookings)
            {
                // Mỗi booking có thể có nhiều BookingFlight
                foreach (var bf in booking.BookingFlights)
                {
                    var flight = bf.Flight;

                    // Và mỗi BookingFlight có thể có nhiều Ticket (nhiều passenger)
                    foreach (var ticket in bf.Tickets)
                    {
                        var card = new UCPaidTicket();

                        // full chiều ngang flowpanel + padding dưới cho có khoảng cách
                        card.Margin = new Padding(0, 0, 0, 12);
                        card.Width = fpnlTransactionHolder.ClientSize.Width - card.Margin.Horizontal;

                        // Dùng lại UI & logic từ UCPaidTicket
                        card.SetData(booking, flight, ticket);

                        fpnlTransactionHolder.Controls.Add(card);
                    }
                }
            }
        }
    }
}
