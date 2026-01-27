using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UCRefundRescheduleContent : UserControl
    {
        private Booking _booking;
        private Ticket _ticket;
        private TicketDetailController? _ticketDetailController;
        public event Action OnRescheduleSuccess;

        public UCRefundRescheduleContent()
        {
            InitializeComponent();
        }

        public async Task SetDataAsync(Booking booking, Ticket ticket)
        {
            _booking = booking;

            // 1. Lấy controller từ DI trước
            _ticketDetailController = DIContainer.TicketDetailController
                ?? throw new InvalidOperationException("TicketDetailController chưa được cấu hình trong DIContainer.");

            // 2. Load lại ticket full navigation
            _ticket = await _ticketDetailController.GetFullTicketAsync(ticket.TicketId);

            if (_ticket == null) throw new Exception("_ticket NULL");
            if (_ticket.BookingFlight == null) throw new Exception("BookingFlight NULL");
            if (_ticket.BookingFlight.Flight == null) throw new Exception("Flight NULL");
            if (_ticket.BookingFlight.Flight.DepartureAirport == null) throw new Exception("DepAirport NULL");
            if (_ticket.BookingFlight.Flight.ArrivalAirport == null) throw new Exception("ArrAirport NULL");
            _ticketDetailController = DIContainer.TicketDetailController;
        }

        private async void btnReschedule_Click(object sender, EventArgs e)
        {
            if (_ticket == null || _booking == null)
            { 
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Đổi lịch không thành công", "Không có dữ liệu vé", false, null);
                announcementForm1.Show();
                return;
            }

            // 1. Check eligibility
            var eligibility = await _ticketDetailController.GetRescheduleEligibilityAsync(_ticket.TicketId);

            if (!eligibility.CanReschedule)
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Hoàn vé không thành công", "Vé không hoàn được", false, null);
                announcementForm1.Show();
                return;
            }

            // 2. Mở form reschedule
            using (var frm = new FrmRescheduleSelectDate(_ticket, _booking))
            {
                frm.ShowDialog();

                if (frm.RescheduleSucceeded)
                {
                    AnnouncementForm announcementForm1 = new AnnouncementForm();
                    announcementForm1.SetAnnouncement("Đổi lịch thành công", "Vé của bạn đã được đổi lịch", true, null);
                    announcementForm1.Show();
                    // Gọi method reload dữ liệu ngoài UCPaidTickets nếu bạn có
                    OnRescheduleSuccess?.Invoke();
                }
            }
        }
    }
}
