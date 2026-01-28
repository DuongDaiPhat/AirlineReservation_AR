using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common;
using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;
using Guna.UI2.WinForms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    public partial class ForgotPassword : Form
    {
        private LoadingForm _loadingForm;
        private string _email;

        private bool _isEmailSent = true;
        private bool _isOtp = false;
        private bool _newPassword = false;

        //HttpClient dùng chung
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5080/v1/api/EmailAPI/")
        };

        public ForgotPassword()
        {
            InitializeComponent();

        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );


        private async void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoading();

                //  Request OTP
                if (_isEmailSent && !_isOtp && !_newPassword)
                {
                    _email = Email.Text;

                    await RequestOtpAsync(_email);

                    otpCard1.Visible = true;
                    EmailLb.Visible = false;
                    Email.Visible = false;
                    _isEmailSent = false;
                    But1.FillColor = Color.White;
                    But1.ForeColor = Color.Gray;
                    But2.FillColor = Color.FromArgb(255, 128, 0);
                    But2.ForeColor = Color.White;
                }
                // Verify OTP
                else if (!_isEmailSent && !_isOtp && !_newPassword)
                {
                    string otp = ""; // bạn lấy OTP từ textbox
                    otp = otpCard1.getOTP().Trim();
                    if (otp == null || otp.Length < 6)
                    {
                        AnnouncementForm announcementForm = new AnnouncementForm();
                        announcementForm.SetAnnouncement(
                            "Error",
                            "The OTP is unavailable",
                            false,
                            null
                        );
                        announcementForm.Show();

                        return;
                    }

                    await VerifyOtpAsync(_email, otp);
                    otpCard1.Visible = false;
                    guna2HtmlLabel6.Visible = true;
                    Email.Visible = true;
                    EmailLb.Text = "New Password";
                    Password.Text = "New Password";
                    Email.PlaceholderText = "New Password";
                    guna2HtmlLabel7.Text = "The new password must not be the same as the old password";
                    guna2HtmlLabel8.Text = ", must have at least 8 characters,";
                    guna2HtmlLabel6.Text = "special characters (@, *,...), uppercase letters (A-Z), and numbers (1-9).";
                    But2.FillColor = Color.White;
                    But2.ForeColor = Color.Gray;
                    But3.FillColor = Color.FromArgb(255, 128, 0);
                    But3.ForeColor = Color.White;
                    _isOtp = true;
                }
                // Reset password
                else if (!_isEmailSent && _isOtp && !_newPassword)
                {
                    string newPassword = ""; //  bạn lấy NewPassword từ textbox

                    newPassword = Email.Text.Trim();
                    if (!ValidatePassword(newPassword, out string errorMessage))
                    {
                        AnnouncementForm announcementForm = new AnnouncementForm();
                        announcementForm.SetAnnouncement(
                            "Error",
                            errorMessage,
                            false,
                            null
                        );
                        announcementForm.Show();
                        return;
                    }
                    await ResetPasswordAsync(_email, newPassword);

                    _newPassword = true;

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                AnnouncementForm announcementForm = new AnnouncementForm();
                announcementForm.SetAnnouncement(
                    "Error",
                    ex.Message,
                    false,
                    null
                );
                announcementForm.Show();
            }
            finally
            {
                CloseLoading();
            }
        }

        // ===============================
        // API CALLS
        // ===============================
        private async Task RequestOtpAsync(string email)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "request",
                new { Email = email }
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        private async Task VerifyOtpAsync(string email, string otp)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "verify",
                new
                {
                    Email = email,
                    Otp = otp
                }
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        private async Task ResetPasswordAsync(string email, string newPassword)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "reset",
                new
                {
                    Email = email,
                    NewPassword = newPassword
                }
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        // ===============================
        // LOADING FORM
        // ===============================
        private void CloseLoading()
        {
            if (_loadingForm != null && !_loadingForm.IsDisposed)
            {
                _loadingForm.Close();
                _loadingForm = null;
            }
        }

        private void ShowLoading()
        {
            if (_loadingForm == null || _loadingForm.IsDisposed)
            {
                _loadingForm = new LoadingForm();
                _loadingForm.Show();
                _loadingForm.BringToFront();
            }
        }

        private bool ValidatePassword(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Password cannot be empty.";
                return false;
            }

            if (password.Length < 8)
            {
                errorMessage = "Password must be at least 8 characters long.";
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                errorMessage = "Password must contain at least one uppercase letter (A-Z).";
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                errorMessage = "Password must contain at least one number (0-9).";
                return false;
            }

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                errorMessage = "Password must contain at least one special character (@, *, !, #, ...).";
                return false;
            }

            return true;
        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
