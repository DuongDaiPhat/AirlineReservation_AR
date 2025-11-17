//using AirlineReservation.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation.src.AirlineReservation.Shared.Utils;
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
using AirlineReservation_AR.Properties;

namespace AirlineReservation.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    public partial class SignInForm : Form
    {
        private readonly Validation validation = new Validation();
        private readonly PasswordHasher hasher = new PasswordHasher();
        //private readonly AirlineReservationDbContext dbContext;

        public SignInForm( )//AirlineReservationDbContext db)
        {

            InitializeComponent();
            //dbContext = db;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1280, 800);
            this.Load += SignInForm_Load;
        }

        public void SignInForm_Load(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = true; // mặc định che
            showPassword.Image = Resources.view; // mặc định eye open
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
            // 1. Validate thông tin đầu vào
            if (emailTB.Text == "" && passwordTB.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin yêu câu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!validation.IsValidGoogleEmail(emailTB.Text)) return;
            if (!validation.IsValidPassword(passwordTB.Text)) return;

            LoadingForm loading = new LoadingForm();
            loading.Show();
            loading.Refresh();

            // 2. Truy vấn DB trong Task tránh UI bị đơ
            //var user = await Task.Run(() =>
            //{
            //    return dbContext.Users.SingleOrDefault(t => t.Email == emailTB.Text.Trim());
            //});

            // 3. Đóng loading NGAY SAU KHI truy vấn xong
            loading.Close();
            loading.Dispose();
            loading = null;

            // 4. Xử lý kết quả
            //if (user == null)
            //{
            //    var errorAnnouncement = new AnnouncementForm();
            //    errorAnnouncement.SetAnnouncement("Error", "Email không tồn tại trong hệ thống.", false, null);
            //    errorAnnouncement.ShowDialog();
            //    return;
            //}

            //if (!hasher.VerifyPassword(passwordTB.Text, user.PasswordHash))
            //{
            //    var errorAnnouncement = new AnnouncementForm();
            //    errorAnnouncement.SetAnnouncement("Error", "Sai mật khẩu.", false, null);
            //    errorAnnouncement.ShowDialog();
            //    return;
            //}

            // 5. Thành công - chuyển sang Form1
            //Form1 form1 = new Form1();
            //var successAnnouncement = new AnnouncementForm();
            //successAnnouncement.SetAnnouncement("Success", "Sign In Successful!", true, form1);
            //successAnnouncement.ShowDialog();

            //this.Hide();
        }

        private void ForgotPS_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            this.Hide();
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
    }
}
