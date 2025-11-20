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
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void btnMyBooking_Click(object sender, EventArgs e)
        {
            LoadMyBookingPage();
        }

        private void LoadMyBookingPage()
        {
            pnlContent.Controls.Clear();

            UCMyBookingPage page = new UCMyBookingPage();

            page.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(page);

            //page.ShowEmptyState();
        }
    }
}
