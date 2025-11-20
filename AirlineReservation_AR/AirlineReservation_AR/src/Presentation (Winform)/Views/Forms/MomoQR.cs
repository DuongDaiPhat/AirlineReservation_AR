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
        public MomoQR()
        {
            InitializeComponent();
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
            string amount = txtAmount.Text.Trim();
            if (string.IsNullOrEmpty(amount))
            {
                MessageBox.Show("Please enter an amount!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (long.TryParse(amount, out long amountValue) && amountValue > 0)
            {
                MessageBox.Show($"Processing payment of {amountValue:N0} VND...\n\nRedirecting to Momo...",
                    "Payment Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var momo = new MomoService();
                await momo.CreatePaymentAsync(amountValue);
            }
            else
            {
                MessageBox.Show("Please enter a valid amount!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
