using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class UC_Header : UserControl
    {
        public event Action<UserDTO> MyTicketClick;
        public event Action BookingClick;
        public event Action HomeClick;
        public event Action PromotionClick;

        public UC_Header()
        {
            InitializeComponent();
        }

        private void btnMyTickets_Click(object sender, EventArgs e)
        {
            var user = DIContainer.CurrentUser;
            var p = new UserDTO
            {
                UserId = user.UserId,

                UserName = user.FullName,

                Email = user.Email,

                Phone = user.Phone

            };
            MyTicketClick.Invoke(p);
        }

        private void btnBookings_Click(object sender, EventArgs e)
        {
            BookingClick.Invoke();
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            HomeClick.Invoke();
        }

        private void btnPromotion_Click(object sender, EventArgs e)
        {
            PromotionClick?.Invoke();
        }
    }
}
