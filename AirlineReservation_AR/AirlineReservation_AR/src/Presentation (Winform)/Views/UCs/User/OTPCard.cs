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
    public partial class OTPCard : UserControl
    {
        private string otp = "";
        public OTPCard()
        {
            InitializeComponent();
        }

        private void txtOtp_TextChanged(object sender, EventArgs e)
        {
            if (sender is not TextBox current)
                return;

            if (current.Text.Length == 1)
            {
                this.SelectNextControl(current, true, true, true, true);
            }
        }


        private void txtOtp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtOtp_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is not TextBox current)
                return;

            if (e.KeyCode == Keys.Back && current.Text == "")
            {
                this.SelectNextControl(current, false, true, true, true);
            }
        }


        public string getOTP()
        {
            otp = txtOtp1.Text + txtOtp2.Text + txtOtp3.Text + txtOtp4.Text + txtOtp5.Text + txtOtp6.Text;
            return otp;
        }
    }
}
