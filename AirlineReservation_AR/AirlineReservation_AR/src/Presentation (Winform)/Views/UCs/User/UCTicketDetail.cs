using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
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
    public partial class UCTicketDetail : UserControl
    {
        public event Action OpenETicketRequest;
        public event Action OpenRefundRescheduleRequest;
        private Booking _booking;
        private Flight _flight;
        private Ticket _ticket;
        public UCTicketDetail()
        {
            InitializeComponent();

            btnETickets.Click += (s, e) => ShowETicket();
            btnRefundReschedule.Click += (s, e) => ShowRefundReschedule();
            btnUpAdd.Click += (s, e) => ShowUpgradesAddons();
        }

        public void SetData(Booking booking, Flight flight, Ticket ticket)
        {
            if (booking == null || flight == null || ticket == null) return;

            _booking = booking;
            _flight = flight;
            _ticket = ticket;

            // Hiển thị Booking ID
            txtBookingID.Text = $"Booking ID: {booking.BookingReference}";

            // Hiển thị Route: SGN -> BKK
            string fromCode = flight.DepartureAirport?.IataCode ?? "???";
            string toCode = flight.ArrivalAirport?.IataCode ?? "???";
            txtRoute.Text = $"{fromCode} -> {toCode}";

            // Mặc định: mở E-ticket (hoặc tab ông muốn)
            ShowETicket();
        }

        private void LoadContent(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }

        private void ShowETicket()
        {
            if (_booking == null || _flight == null || _ticket == null) return;

            var summary = new FlightTicketSummary();
            summary.SetData(_booking, _flight, _ticket);
            LoadContent(summary);
        }

        private void ShowRefundReschedule()
        {
            if (_booking == null || _ticket == null) return;

            var refundPage = new UCRefundRescheduleContent();
            refundPage.SetData(_booking, _ticket); // nếu UC này cần data thì thêm sau
            LoadContent(refundPage);
        }

        private void ShowUpgradesAddons()
        {
            // TODO: Tạo UCUpgradesAddons riêng rồi nhét vào đây
            // var upgrades = new UCUpgradesAddons();
            // upgrades.SetData(_booking, _flight, _ticket);
            // LoadContent(upgrades);
        }

    }
}
