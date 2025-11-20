using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AR_Winform.Presentation.UControls.User
{
    public partial class TypeTicket : UserControl
    {
        public event EventHandler<(int Adult, int Child, int Infant)> DoneClicked;
        public TypeTicket()
        {
            InitializeComponent();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            int adult = (int)adultNumeric.Value;
            int child = (int)childNumeric.Value;
            int infant = (int)infantNumeric.Value;

            DoneClicked?.Invoke(this, (adult, child, infant));
            this.Hide();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
