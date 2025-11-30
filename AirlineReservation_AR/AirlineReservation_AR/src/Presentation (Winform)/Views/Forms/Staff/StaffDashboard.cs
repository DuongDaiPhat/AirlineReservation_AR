using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Staff
{
    public partial class StaffDashboard : Form
    {
        private UC_Staffdashboard _staffUc;

        public StaffDashboard()
        {
            InitializeComponent();
        }

        private void StaffDashboard_Load(object sender, EventArgs e)
        {
            _staffUc = new UC_Staffdashboard();
            _staffUc.Dock = DockStyle.Fill;

            bodyPnl.Controls.Clear();
            bodyPnl.Controls.Add(_staffUc);

            uC_StaffHeader.MenuToggleRequested += () =>
            {
                _staffUc.ToggleSidebar();
            };
        }
    }
}
