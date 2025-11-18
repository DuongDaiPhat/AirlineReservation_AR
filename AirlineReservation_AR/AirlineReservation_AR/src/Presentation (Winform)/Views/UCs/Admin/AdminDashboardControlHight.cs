using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    public partial class AdminDashboardControlHight : UserControl
    {
        public AdminDashboardControlHight()
        {
            InitializeComponent();
        }
        public string title
        {
            get { return labelControlHight.Text; }
            set { labelControlHight.Text = value; }
        }
        public Image dashboardImage
        {
            get { return picBoxControlHight.Image; }
            set { picBoxControlHight.Image = value; }
        }
        public void SetCardData(String title, Image dashboardImage)
        {
            this.title = title;
            this.dashboardImage = dashboardImage;
        }
        private void AdminDashboardControlHight_Load(object sender, EventArgs e)
        {

        }
    }
}
