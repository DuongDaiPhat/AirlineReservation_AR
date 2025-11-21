using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;
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
    public partial class UserDashboard : Form
    {
        private readonly UserService _userService = new UserService();
        public UserDashboard()
        {
            InitializeComponent();

            this.Load += UserDashboard_Load;
        }

        private async void UserDashboard_Load(object? sender, EventArgs e)
        {
            await LoadUserInfoAsync();
        }
        private void btnMyBooking_Click(object sender, EventArgs e)
        {
            LoadMyBookingPage();
        }

        private void LoadMyBookingPage()
        {
            pnlContent.Controls.Clear();

            UCMyBookingPage page = new UCMyBookingPage();

            page.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(page);

            //page.ShowEmptyState();
        }

        private async Task LoadUserInfoAsync()
        {
            // Nếu UserSession chưa set thì thôi, tránh null
            if (!UserSession.IsInitialized)
                return;

            var user = await _userService.GetByIdAsync(UserSession.UserId);
            if (user == null)
                return;

            // Đổ dữ liệu ra UI
            txtName.Text = user.FullName;
            txtEmail.Text = user.Email;

            guna2CircleButton1.Text = GetUserAcronym(user.FullName);
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

            // Lấy 2 ký tự đầu
            return letters.Substring(0, 2);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            var uc = new UCAccountModify();
            uc.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(uc);
        }

        public void RefreshUserInfoOnNav()
        {
            txtName.Text = UserSession.FullName ?? "";
            txtEmail.Text = UserSession.Email ?? "";

            // Nếu em có circle button hiển thị 2 chữ cái tên:
            string initials = "";
            if (!string.IsNullOrWhiteSpace(UserSession.FullName))
            {
                var parts = UserSession.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                    initials = $"{char.ToUpper(parts[parts.Length - 2][0])}{char.ToUpper(parts[parts.Length - 1][0])}";
                else
                    initials = char.ToUpper(parts[0][0]).ToString();
            }

            guna2CircleButton1.Text = initials; // hoặc cbtnUserAcronym nếu em đổi tên
        }

        private void btnPurchaseList_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            var uc = new UCUserTransaction();
            uc.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(uc);
        }
    }
}
