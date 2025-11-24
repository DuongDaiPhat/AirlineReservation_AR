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
        public event Action PromotionClick;        public event Action<UserDTO> OpenMyBookingRequest;
        public event Action<UserDTO> OpenMyTransactionRequest;
        public event Action<UserDTO> OpenAccountModifyRequest;
        public event Action LogoutRequest;



        private ToolStripDropDown _popup;
        public UC_Header()
        {
            InitializeComponent();

            btnUserProfile.Click += btnUserProfile_Click;


            this.Load += UC_Header_Load;
        }

        private void UC_Header_Load(object? sender, EventArgs e)
        {

            LoadUI();
        }

        public void LoadUI()
        {
            btnUserProfile.Text = DIContainer.CurrentUser.FullName;
        }

        private void btnUserProfile_Click(object? sender, EventArgs e)
        {
            if (_popup != null && _popup.Visible)
            {
                _popup.Close();
                return;
            }

            var currentUser = DIContainer.CurrentUser;
            if (currentUser == null) return;

            var userDto = new UserDTO
            {
                UserId = currentUser.UserId,
                UserName = currentUser.FullName,
                Email = currentUser.Email,
                Phone = currentUser.Phone
            };

            var ucProfile = new UCUserProfile(userDto);

            ucProfile.BookingClick += (s, args) =>
            {
                _popup?.Close();

                var user = DIContainer.CurrentUser;
                var p = new UserDTO
                {
                    UserId = user.UserId,

                    UserName = user.FullName,

                    Email = user.Email,

                    Phone = user.Phone

                };

                OpenMyBookingRequest?.Invoke(p);
            };

            ucProfile.PurchaseListClick += (s, args) =>
            {
                _popup?.Close();

                var user = DIContainer.CurrentUser;
                var p = new UserDTO
                {
                    UserId = user.UserId,

                    UserName = user.FullName,

                    Email = user.Email,

                    Phone = user.Phone

                };

                OpenMyTransactionRequest?.Invoke(p);
            };

            ucProfile.AccountClick += (s, args) =>
            {
                _popup?.Close();

                var user = DIContainer.CurrentUser;
                var p = new UserDTO
                {
                    UserId = user.UserId,

                    UserName = user.FullName,

                    Email = user.Email,

                    Phone = user.Phone

                };

                OpenAccountModifyRequest?.Invoke(p);
            };

            ucProfile.LogoutClick += (s, args) =>
            {
                _popup?.Close();

                LogoutRequest?.Invoke();
            };


            ucProfile.Size = new Size(393, 523);

            ToolStripControlHost host = new ToolStripControlHost(ucProfile);

            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false; // Tắt auto size của host để theo size của UC

            _popup = new ToolStripDropDown();
            _popup.Renderer = new NoBorderRenderer();
            _popup.Padding = Padding.Empty;
            _popup.Margin = Padding.Empty;
            _popup.Items.Add(host);


            _popup.AutoClose = true; // Click ra ngoài tự tắt
            _popup.DropShadowEnabled = true; // Hiệu ứng bóng đổ cho đẹp

            Control targetButton = sender as Control;
            if (targetButton != null)
            {

                Point screenPoint = targetButton.PointToScreen(new Point(targetButton.Width - ucProfile.Width, targetButton.Height));

                _popup.Show(screenPoint);
            }
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

        public class NoBorderRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
            }
        }
    }
}
