using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.Properties;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using AirlineReservation_AR.src.Infrastructure.DI;
//using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
//using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    public partial class SignUpForm : Form
    {
        private readonly Validation validation = new Validation();
        private readonly PasswordHasher hasher = new PasswordHasher();
        private readonly AuthenticationController _controller;
        private readonly UserContrller _userContrller;
        public SignUpForm()
        {
            InitializeComponent();
            _controller = DIContainer.AuthController;
            _userContrller = DIContainer.UserContrller;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1280, 800);
            this.Load += SignUpForm_Load;
        }

        public void SignUpForm_Load(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = true; // mặc định che
            confirmPasswordTB.UseSystemPasswordChar = true;
            showPassword.Image = Resources.hide; // mặc định eye open
            showConfirmedPassword.Image = Resources.hide;
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            //using var db = Connection.GetDbContext();
            SignInForm signInForm = new SignInForm();// db);
            signInForm.Show();
            this.Hide();
        }

        private async void SignUpBtn_Click(object sender, EventArgs e)
        {
            if (emailTB.Text == "" && userNameTB.Text == "" && numberTB.Text == "" && passwordTB.Text == "" && confirmPasswordTB.Text == "")
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Đăng ký không thành công", $"Vui lòng điền đầy đủ thông tin", false, null);
                announcementForm1.Show();
                return;
            }
            if (!validation.IsValidGoogleEmail(emailTB.Text)) return;
            if (!validation.IsValidPhoneNumber(numberTB.Text)) return;
            if (!validation.IsValidPassword(passwordTB.Text)) return;
            if (!Equals(passwordTB.Text, confirmPasswordTB.Text))
            {
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Đăng ký không thành công", $"Mật khẩu không đồng bộ", false, null);
                announcementForm1.Show();
                return;
            }

            // 1. Tạo User mới
            var user = await _controller.RegisterAsync(
                userNameTB.Text,
                emailTB.Text,
                passwordTB.Text,
                numberTB.Text);

            if (user == null)
            {
                MessageBox.Show("Đăng ký thất bại! Vui lòng thử lại.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Đăng ký không thành công", $"Sđt, Email hoặc tên tài khoản đã được sử dụng", false, null);
                announcementForm1.Show();
                return;
            }

            var addRole = await _userContrller.AddUserRoleAsync(emailTB.Text, 3);
            if (!addRole.Success)
            {

                AnnouncementForm announcementForm1 = new AnnouncementForm();
                announcementForm1.SetAnnouncement("Đăng ký không thành công", $"Lý do: {addRole.Message}", false, null);
                announcementForm1.Show();
            }

            AnnouncementForm announcementForm = new AnnouncementForm();
            announcementForm.SetAnnouncement("Đăng ký thành công", "Hãy đăng nhập để tiếp tục", true, null);
            announcementForm.Show();

            //Điều hướng sang SignIn
            SignInForm signin = new SignInForm();
            signin.Show();
            this.Hide();
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

        private void showConfirmedPassword_Click(object sender, EventArgs e)
        {
            if (confirmPasswordTB.UseSystemPasswordChar)
            {
                // Hiện mật khẩu
                confirmPasswordTB.UseSystemPasswordChar = false;
                showConfirmedPassword.Image = Resources.view;
            }
            else
            {
                // Ẩn mật khẩu
                confirmPasswordTB.UseSystemPasswordChar = true;
                showConfirmedPassword.Image = Resources.hide;
            }
        }

        private void passwordTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
