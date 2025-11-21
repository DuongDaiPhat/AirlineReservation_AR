using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MomoQR
{
    public partial class MomoQR : Form
    {
        private int _bookingId;
        private decimal _amount;
        public MomoQR()
        {
            InitializeComponent();
        }
        public void SetPayment(int bookingId, decimal amount)
        {
            _bookingId = bookingId;
            _amount = amount;
            txtAmount.Text = amount.ToString("N0");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private async void payButton_Click(object sender, EventArgs e)
        {
            var momo = new MomoService();
            await momo.CreatePaymentAsync(_bookingId, (long)_amount);

            MessageBox.Show("Redirecting to MoMo…");
            this.Close();
        }

        private void payButton_MouseEnter(object sender, EventArgs e)
        {
            payButton.BackColor = Color.FromArgb(200, 35, 51);
        }

        private void payButton_MouseLeave(object sender, EventArgs e)
        {
            payButton.BackColor = Color.FromArgb(220, 53, 69);
        }

    }
}


