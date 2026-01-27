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
            showPassword.Image = Resources.view; // mặc định eye open
            showConfirmedPassword.Image = Resources.view;
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
                MessageBox.Show("Vui lòng điền thông tin yêu câu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!validation.IsValidGoogleEmail(emailTB.Text)) return;
            if (!validation.IsValidPhoneNumber(numberTB.Text)) return;
            if (!validation.IsValidPassword(passwordTB.Text)) return;
            if (!Equals(passwordTB.Text, confirmPasswordTB.Text))
            {
                MessageBox.Show("Mật khẩu không đồng bộ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                return;
            }

            var addRole = await _userContrller.AddUserRoleAsync(emailTB.Text, 3);
            if (!addRole.Success)
            {
                MessageBox.Show("Đăng ký thành công nhưng không thể gán role người dùng.\n" +
                                $"Lý do: {addRole.Message}",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show("Đăng ký thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);


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
    }
}
