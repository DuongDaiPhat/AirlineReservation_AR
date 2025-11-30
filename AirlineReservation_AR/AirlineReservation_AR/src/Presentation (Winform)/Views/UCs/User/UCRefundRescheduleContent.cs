using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
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
    public partial class UCRefundRescheduleContent : UserControl
    {
        private Booking _booking;
        private Ticket _ticket;

        public UCRefundRescheduleContent()
        {
            InitializeComponent();
        }

        public void SetData(Booking booking, Ticket ticket)
        {
            _booking = booking;
            _ticket = ticket;
        }
    }
}
