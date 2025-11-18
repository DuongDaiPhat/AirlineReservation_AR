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
//using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
//using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;

namespace AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    public partial class SignUpForm : Form
    {
        private readonly Validation validation = new Validation();
        private readonly PasswordHasher hasher = new PasswordHasher();
        //private readonly AirlineReservationDbContext dbContext;
        public SignUpForm()//AirlineReservationDbContext db)
        {
            InitializeComponent();
            //dbContext = db;
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

        private void SignUpBtn_Click(object sender, EventArgs e)
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

            //using var db = Connection.GetDbContext();

            // 1. Tạo User mới
            //var newUser = new User
            //{
            //    UserId = Guid.NewGuid(),
            //    FullName = userNameTB.Text.Trim(),
            //    Email = emailTB.Text.Trim(),
            //    Phone = numberTB.Text.Trim(),
            //    PasswordHash = hasher.HashPassword(passwordTB.Text),
            //    CreatedAt = DateTime.Now,
            //    IsActive = true,
            //    IsVerified = false
            //};
            //db.Users.Add(newUser);

            // 2. Gán Role mặc định = 3 - Khách hàng
            //var userRole = new UserRole
            //{
            //    UserId = newUser.UserId,
            //    RoleId = 3,
            //    AssignedAt = DateTime.Now,
            //    AssignedBy = Guid.Parse("D3F9A7C2-8B1E-4F3A-9C2A-7E4F9A1B2C3D")
            //};
            //db.UserRoles.Add(userRole);

            // 3. Lưu thay đổi vào DB
            //db.SaveChanges();

            // 4. Hộp thoại tb thành công 
            DialogResult result = MessageBox.Show(
                "Đăng ký thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.OK)
            {
                SignInForm signin = new SignInForm(); //db);
                signin.Show();
                this.Hide();
            }
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            if (passwordTB.UseSystemPasswordChar)
            {
                // Hiện mật khẩu
                passwordTB.UseSystemPasswordChar = false;
                showPassword.Image = Resources.hide;
            }
            else
            {
                // Ẩn mật khẩu
                passwordTB.UseSystemPasswordChar = true;
                showPassword.Image = Resources.view;
            }
        }

        private void showConfirmedPassword_Click(object sender, EventArgs e)
        {
            if (confirmPasswordTB.UseSystemPasswordChar)
            {
                // Hiện mật khẩu
                confirmPasswordTB.UseSystemPasswordChar = false;
                showConfirmedPassword.Image = Resources.hide;
            }
            else
            {
                // Ẩn mật khẩu
                confirmPasswordTB.UseSystemPasswordChar = true;
                showConfirmedPassword.Image = Resources.view;
            }
        }
    }
}
