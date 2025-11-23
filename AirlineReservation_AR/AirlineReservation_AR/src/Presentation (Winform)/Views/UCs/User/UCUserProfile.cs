using AirlineReservation_AR.src.AirlineReservation.Application.Services;
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
    public partial class UCUserProfile : UserControl
    {
        private UserDTO _user;
        private readonly UserService _userService;
        public EventHandler BookingClick;
        public EventHandler PurchaseListClick;
        public EventHandler AccountClick;
        public EventHandler LogoutClick;
        public UCUserProfile(UserDTO user)
        {
            InitializeComponent();
            _user = user;
            _userService = new UserService();
            this.Load += UserProfile_Load;
        }

        private async void UserProfile_Load(object? sender, EventArgs e)
        {
            await LoadUserInfoAsync();
        }
        private void btnMyBooking_Click(object sender, EventArgs e)
        {
            BookingClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnPurchaseList_Click(object sender, EventArgs e)
        {
            PurchaseListClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            AccountClick?.Invoke(this, EventArgs.Empty);

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LogoutClick?.Invoke(this, EventArgs.Empty);

        }

        private async Task LoadUserInfoAsync()
        {
            if (_user == null)
                return;

            var user = await _userService.GetByIdAsync(_user.UserId);
            if (user == null)
                return;

            txtName.Text = user.FullName;
            txtEmail.Text = user.Email;

            cbtnUserAcronym.Text = GetUserAcronym(user.FullName);
        }

        private string GetUserAcronym(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "?";

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
    }
}
