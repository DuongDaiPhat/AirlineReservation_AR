using AirlineReservation_AR.src.Domain.DTOs;
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
    public partial class UC_PromotionItem : UserControl
    {
        public UC_PromotionItem()
        {
            InitializeComponent();
        }

        public void Bind(PromotionDTO promo)
        {
            // Các label bạn đặt sẵn trong Designer
            proNameLbl.Text = promo.PromoName;               // Discount 012
            proValidFromLbl.Text = $"{promo.ValidFrom:dd MMM yyyy} - {promo.ValidTo:dd MMM yyyy}";
            desc.Text = promo.Description;
            proCodeLbl.Text = promo.PromoCode;                      // DISCOUNT012
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(proCodeLbl.Text))
            {
                Clipboard.SetText(proCodeLbl.Text);
                MessageBox.Show("Promo Code copied to clipboard!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
