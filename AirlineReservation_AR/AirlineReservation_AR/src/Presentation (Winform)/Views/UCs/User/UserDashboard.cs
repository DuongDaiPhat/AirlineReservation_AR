using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
using Guna.UI2.WinForms;
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
    public partial class UserDashboard : UserControl
    {
        private UserDTO _user;
        private UserService _userService;
        public UserDashboard(UserDTO user)
        {
            InitializeComponent();

            _user = user;
            _userService = new UserService();
            this.Load += UserDashboard_Load;
            btnMyBooking.Click += btnMyBooking_Click;
            btnAccount.Click += btnAccount_Click;
            btnPurchaseList.Click += btnPurchaseList_Click;
        }

        private async void UserDashboard_Load(object? sender, EventArgs e)
        {
            await LoadUserInfoAsync();
        }
        private void btnMyBooking_Click(object sender, EventArgs e)
        {
            LoadMyBookingPage();
        }
        private void btnPurchaseList_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            var uc = new UCUserTransaction(_user);
            uc.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(uc);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            var uc = new UCAccountModify(_user);
            uc.Dock = DockStyle.Fill;
            uc.AccountUpdated += UcAccountModify_AccountUpdated;

            pnlContent.Controls.Add(uc);
        }
        private async void  UcAccountModify_AccountUpdated(object? sender, EventArgs e)
        {
            await LoadUserInfoAsync();
        }

        private void LoadMyBookingPage()
        {
            pnlContent.Controls.Clear();

            UCMyBookingPage page = new UCMyBookingPage(_user);

            page.Dock = DockStyle.Fill;

            page.CheckTransactionClick += btnPurchaseList_Click;

            pnlContent.Controls.Add(page);

            //page.ShowEmptyState();
        }

        private async Task LoadUserInfoAsync()
        {
            if (_user == null)
                return;

            var user = await _userService.GetByIdAsync(_user.UserId);
            if (user == null)
                return;

            // Đổ dữ liệu ra UI
            txtName.Text = user.FullName;
            txtEmail.Text = user.Email;

            cbtnUserAcronym.Text = GetUserAcronym(user.FullName);
        }

        private string GetUserAcronym(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "?";

            // Lọc chỉ giữ lại chữ cái
            var letters = new string(fullName
                .Where(char.IsLetter)
                .ToArray());

            if (string.IsNullOrWhiteSpace(letters))
                return "?";

            letters = letters.ToUpperInvariant();

            if (letters.Length == 1)
                return letters.Substring(0, 1);

            return letters.Substring(0, 2);
        }

       

        public void RefreshUserInfoOnNav()
        {
            txtName.Text = _user.UserName ?? "";
            txtEmail.Text = _user.Email ?? "";

            // Nếu em có circle button hiển thị 2 chữ cái tên:
            string initials = "";
            if (!string.IsNullOrWhiteSpace(_user.UserName))
            {
                var parts = _user.UserName?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts?.Length >= 2)
                    initials = $"{char.ToUpper(parts[parts.Length - 2][0])}{char.ToUpper(parts[parts.Length - 1][0])}";
                else
                    initials = char.ToUpper(parts[0][0]).ToString();
            }

            cbtnUserAcronym.Text = initials; // hoặc cbtnUserAcronym nếu em đổi tên
        }

        

    }
}
