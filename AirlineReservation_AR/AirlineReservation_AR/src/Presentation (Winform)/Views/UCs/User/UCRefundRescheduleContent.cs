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
                announcementForm1.SetAnnouncement("Reschedule failed", "No ticket data", false, null);
                announcementForm1.Show();
                return;
            }

            // 1. Check eligibility
            var eligibility = await _ticketDetailController.GetRescheduleEligibilityAsync(_ticket.TicketId);

            if (!eligibility.CanReschedule)
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Refund failed", "Ticket cannot be refunded", false, null);
                announcementForm1.Show();
                return;
            }

            // 2. Open reschedule form
            using (var frm = new FrmRescheduleSelectDate(_ticket, _booking))
            {
                frm.ShowDialog();

                if (frm.RescheduleSucceeded)
                {
                    AnnouncementForm announcementForm1 = new AnnouncementForm();
                    announcementForm1.SetAnnouncement("Reschedule successful", "Your ticket has been rescheduled", true, null);
                    announcementForm1.Show();
                    // Call method to reload data outside UCPaidTickets if you have
                    OnRescheduleSuccess?.Invoke();
                }
            }
        }
    }
}
