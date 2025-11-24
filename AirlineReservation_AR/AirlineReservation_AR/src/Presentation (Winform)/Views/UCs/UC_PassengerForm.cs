using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs
{
    public partial class UC_PassengerForm : UserControl
    {
        public UC_PassengerForm()
        {
            InitializeComponent();
        }

        private void lnkNameGuideline_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Name guideline information will be displayed here.", "Name Guideline");
        }

        private void lnkLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("More information about passport requirements.", "Learn More");
        }
    }
}
