//using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.Properties;
using AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.Forms.Admin;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Staff;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    public partial class SignInForm : Form
    {
        private readonly Validation validation = new Validation();
        private readonly PasswordHasher hasher = new PasswordHasher();
        private readonly AuthenticationController _controller;

        public SignInForm()//AirlineReservationDbContext db)
        {

            InitializeComponent();
            _controller = DIContainer.AuthController;
            //dbContext = db;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1280, 800);
            this.Load += SignInForm_Load;
        }

        public void SignInForm_Load(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = true; // mặc định che
            showPassword.Image = Resources.hide; // mặc định eye open
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            //using var db = Connection.GetDbContext();
            SignUpForm signUpForm = new SignUpForm();//db);
            signUpForm.Show();
            this.Hide();
        }

        private async void SignInBtn_Click(object sender, EventArgs e)
        {
            // 1. Validate input information
            if (string.IsNullOrWhiteSpace(emailTB.Text) ||
                string.IsNullOrWhiteSpace(passwordTB.Text))
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Login failed", "Please enter email or password", false, null);
                announcementForm1.Show();
                announcementForm1.BringToFront();

                return;
            }

            if (!validation.IsValidGoogleEmail(emailTB.Text))
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Login failed", "Invalid email", false, null);
                announcementForm1.Show();
                announcementForm1.BringToFront();

                return;
            }

            if (!validation.IsValidPassword(passwordTB.Text))
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Login failed", "Invalid password", false, null);
                announcementForm1.Show();
                announcementForm1.BringToFront();

                return;
            }

            LoadingForm loading = new LoadingForm();
            loading.Show();
            loading.Refresh();




            // 2. Truy vấn DB trong Task tránh UI bị đơ
            var result = await _controller.LoginAsync(emailTB.Text, passwordTB.Text);


            // 3. Đóng loading NGAY SAU KHI truy vấn xong
            loading.Close();
            loading.Dispose();
            loading = null;

            // 3. Process result
            if (result == null)
            {
                var errorAnnouncement = new AnnouncementForm();
                errorAnnouncement.SetAnnouncement(
                    "Error",
                    "Email or password is incorrect.",
                    false,
                    null);
                errorAnnouncement.ShowDialog();
                errorAnnouncement.BringToFront();
                return;
            }

            var user = result.User;
            var roleId = result.RoleId;

            // 4. Lưu thông tin user hiện tại vào DIContainer
            DIContainer.SetCurrentUser(user);

            // 5. Chuyển đến form tương ứng theo role
            Form nextForm;

            switch (roleId)
            {
                case 1:
                    nextForm = new MenuAdminDashboard();   // Admin
                    break;
                case 2:
                    nextForm = new StaffDashboard();       // Staff
                    break;
                case 3:
                default:
                    nextForm = new MainTravelokaForm();    // User
                    break;
            }

            // 6. Thông báo success + mở form tương ứng
            var successAnnouncement = new AnnouncementForm();
            successAnnouncement.SetAnnouncement("Success", "Sign In Successful!", true, nextForm);
            successAnnouncement.ShowDialog();

            this.Hide();
        }

        private void ForgotPS_Click(object sender, EventArgs e)
        {
            var ForgotPasswordForm = new ForgotPassword();
            ForgotPasswordForm.StartPosition = FormStartPosition.CenterScreen;
            ForgotPasswordForm.ShowDialog();
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            if (passwordTB.UseSystemPasswordChar)
            {
                // Hiện mật khẩu
                passwordTB.UseSystemPasswordChar = false;
                showPassword.Image = Resources.view;

            }
            else
            {
                // Ẩn mật khẩu
                passwordTB.UseSystemPasswordChar = true;
                showPassword.Image = Resources.hide;

            }
        }


    }
}
