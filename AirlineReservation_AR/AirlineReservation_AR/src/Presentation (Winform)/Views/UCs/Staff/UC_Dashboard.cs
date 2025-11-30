using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Staff
{
    public partial class UC_Dashboard : UserControl
    {
        private readonly StaffDashboardController _dashboardController;

        public UC_Dashboard()
        {
            InitializeComponent();

            _dashboardController = DIContainer.StaffDashboardController;
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            var staff = DIContainer.CurrentUser;
            if (staff == null) return;

            var stats = _dashboardController.GetTodayStats(staff.UserId);

            ticketProcessedLbl.Text = stats.TicketsProcessed.ToString();
            checkinLbl.Text = stats.CheckinCompleted.ToString();
            progressLbl.Text = stats.InProgress.ToString();
            //reportedIssuesLbl.Text = stats.ReportedIssues.ToString();

            // progress bar “Today’s Work Progress”
            if (stats.TotalTicketsToday > 0)
            {
                int percent = (int)Math.Round(
                    stats.CompletedTicketsToday * 100.0 / stats.TotalTicketsToday);

                percenTicketBar.Value = Math.Max(0, Math.Min(100, percent));
                totalTicketLbl.Text = $"{stats.CompletedTicketsToday}/{stats.TotalTicketsToday} tickets";
            }
            else
            {
                percenTicketBar.Value = 0;
                totalTicketLbl.Text = "0/0 tickets";
            }
        }
    }
}
