using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    public partial class UCMyBookingPage : UserControl
    {
        public UCMyBookingPage()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            LoadPendingDemo();

            LoadPaidDemo();

            fpnlIssuedTicketHolder.AutoScroll = true;
            fpnlIssuedTicketHolder.FlowDirection = FlowDirection.TopDown;
            fpnlIssuedTicketHolder.WrapContents = false;

            fpnlPendingTicketHolder.AutoScroll = true;
            fpnlPendingTicketHolder.FlowDirection = FlowDirection.TopDown;
            fpnlPendingTicketHolder.WrapContents = false;
        }

       
        public void LoadPendingDemo()
        {
            panelPendingOrders.Visible = true;

            fpnlPendingTicketHolder.Controls.Clear();

            for (int i = 0; i < 3; i++)
            {
                var card = new UCPendingBookingCard();

                card.Margin = new Padding(0, 0, 0, 10);

                fpnlPendingTicketHolder.Controls.Add(card);
            }


        }

        public void LoadPaidDemo()
        {
            pnlNoIssuedTicket.Visible = false;

            fpnlIssuedTicketHolder.Controls.Clear();

            for (int i = 0; i < 3; i++)
            {
                var card = new UCPaidTicket();

                card.Margin = new Padding(0, 0, 0, 10);

                fpnlIssuedTicketHolder.Controls.Add(card);
            }

        }
        public void ShowEmptyState()
        {
            panelPendingOrders.Visible = false;

            fpnlIssuedTicketHolder.Visible = false;

            pnlNoIssuedTicket.Visible = true;
        }

        private void fpnlIssuedTicketHolder_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
