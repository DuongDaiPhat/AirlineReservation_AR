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
