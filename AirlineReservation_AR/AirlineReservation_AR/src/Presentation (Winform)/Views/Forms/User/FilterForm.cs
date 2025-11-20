using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AR_Winform.Presentation.Forms
{
    public partial class FilterForm : Form
    {
        private string[] sortOptions = {
            "Earliest departure",
            "Latest departure",
            "Earliest arrival",
            "Latest arrival"
        };
        private bool _isRoundTrip = false;

        public FilterForm()
        {
            InitializeComponent();

            // Gắn "Others" làm mặc định
            sortCB.Items.Add("Others");
            sortCB.SelectedIndex = 0;

            // Set initial value for label
            UpdatePriceLabel();

            // Hook up event for trackBarPrice
            this.trackBarPrice.ValueChanged += new System.EventHandler(this.trackBarPrice_ValueChanged);
        }

        private void trackBarPrice_ValueChanged(object sender, EventArgs e)
        {
            UpdatePriceLabel();
        }

        private void UpdatePriceLabel()
        {
            if (labelPriceValue != null && trackBarPrice != null)
            {
                // TrackBar range is 0 to 950
                // Each step represents 10,000 VND
                // Min Price = 500,000
                // Max Price = 500,000 + (950 * 10,000) = 10,000,000
                
                double scaledValue = trackBarPrice.Value;
                double actualPrice = 500000 + (scaledValue * 10000);
                
                // Format as currency Vietnamese Dong
                labelPriceValue.Text = string.Format("{0:N0} VND - {1:N0} VND", 500000, actualPrice);
            }
        }

        // ====== COMBOBOX SORT MỞ DANH SÁCH ======
        private void sortCB_DropDown(object sender, EventArgs e)
        {
            if (sortCB.Items.Contains("Others"))
            {
                sortCB.Items.Clear();
                sortCB.Items.AddRange(sortOptions);
            }
        }


    }
}
