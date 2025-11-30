using AirlineReservation_AR.src.Infrastructure.DI;
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
    public partial class UC_StaffHeader : UserControl
    {
        // Sự kiện yêu cầu mở/đóng menu
        public event Action MenuToggleRequested;

        public UC_StaffHeader()
        {
            InitializeComponent();
        }

        private void UC_StaffHeader_Load(object sender, EventArgs e)
        {
            LoadUI();
        }

        public void LoadUI()
        {
            var user = DIContainer.CurrentUser;
            if (user != null)
                btnUserProfile.Text = user.FullName;
        }

        private void btnMenu_Click(object sender, EventArgs e) => MenuToggleRequested?.Invoke();
    }
}
