using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.Infrastructure.DI;
using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
using AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User;
using Microsoft.EntityFrameworkCore;
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
    public partial class UCAccountModify : UserControl
    {
        private readonly UserService _userService = new UserService();

        private bool _isLoading = false;
        public UCAccountModify()
        {
            InitializeComponent();

            this.Load += UCAccountModify_Load;

            // Gender combo basic
            cboGender.DisplayMember = "Text";
            cboGender.ValueMember = "Value";
            cboGender.DataSource = new[]
            {
                new { Text = "Male", Value = "M" },
                new { Text = "Female", Value = "F" },
                new { Text = "Other", Value = "O" }
            };


            // Email & UserId chỉ xem, không sửa
            txtEmail.ReadOnly = true;

            txtPhone.KeyPress += TxtPhone_KeyPress;
            txtPhone.TextChanged += txtPhone_TextChanged;
        }

        private async void UCAccountModify_Load(object sender, EventArgs e)
        {
            if (UserSession.UserId == Guid.Empty)
            {
                MessageBox.Show("User session is empty.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _isLoading = true;
            try
            {
                await LoadCountriesAsync();
                await LoadUserProfileAsync();
            }
            finally
            {
                _isLoading = false;
            }

            // event sau khi đã load country
            cboCountry.SelectedIndexChanged += cboCountry_SelectedIndexChanged;
            btnSaveChanges.Click += btnSaveChanges_Click;
        }

        private void TxtPhone_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Cho phép phím control (Backspace, Delete, mũi tên...)
            if (char.IsControl(e.KeyChar))
                return;

            // Cho phép số
            if (char.IsDigit(e.KeyChar))
                return;

            // Nếu muốn cho phép dấu + ở đầu:
            if (e.KeyChar == '+' && txtPhone.SelectionStart == 0 && !txtPhone.Text.Contains("+"))
                return;

            // Còn lại: chặn
            e.Handled = true;
        }
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            var digitsOnly = new string(txtPhone.Text.Where(char.IsDigit).ToArray());
            if (digitsOnly != txtPhone.Text)
            {
                int caret = txtPhone.SelectionStart;
                txtPhone.Text = digitsOnly;
                txtPhone.SelectionStart = Math.Min(caret, txtPhone.Text.Length);
            }
        }
        private async Task LoadCountriesAsync()
        {
            using var db = DIContainer.CreateDb();

            var countries = await db.Countries
                .OrderBy(c => c.CountryName)
                .ToListAsync();

            cboCountry.DisplayMember = "CountryName";
            cboCountry.ValueMember = "CountryCode";
            cboCountry.DataSource = countries;
        }

        private async Task LoadCitiesByCountryAsync(string countryCode)
        {
            using var db = DIContainer.CreateDb();

            var cities = await db.Cities
                .Where(c => c.CountryCode == countryCode)
                .OrderBy(c => c.CityName)
                .ToListAsync();

            cboCity.DisplayMember = "CityName";
            cboCity.ValueMember = "CityCode";
            cboCity.DataSource = cities;
        }

        private async Task LoadUserProfileAsync()
        {
            var user = await _userService.GetByIdAsync(UserSession.UserId);
            if (user == null)
            {
                MessageBox.Show("No user found.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFullName.Text = user.FullName ?? "";
            txtEmail.Text = user.Email ?? "";
            txtPhone.Text = user.Phone ?? "";
            txtAddress.Text = user.Address ?? "";


            if (!string.IsNullOrWhiteSpace(user.Gender))
            {
                cboGender.SelectedValue = user.Gender;
            }

            // Xử lý country / city theo CityCode của user
            using var db = DIContainer.CreateDb();
            if (!string.IsNullOrWhiteSpace(user.CityCode))
            {
                var city = await db.Cities
                    .Include(c => c.Country)
                    .FirstOrDefaultAsync(c => c.CityCode == user.CityCode);

                if (city != null)
                {
                    // set country
                    cboCountry.SelectedValue = city.CountryCode;
                    // load city theo country
                    await LoadCitiesByCountryAsync(city.CountryCode);
                    // set city
                    cboCity.SelectedValue = user.CityCode;
                }
            }
            else
            {
                // Nếu user chưa có city, default country = VN
                var vn = await db.Countries
                    .FirstOrDefaultAsync(c => c.CountryCode == "VNM");
                if (vn != null)
                {
                    cboCountry.SelectedValue = vn.CountryCode;
                    await LoadCitiesByCountryAsync(vn.CountryCode);
                }
            }
        }

        // ================== EVENTS ==================

        private async void cboCountry_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;

            if (cboCountry.SelectedValue is string countryCode &&
                !string.IsNullOrWhiteSpace(countryCode))
            {
                await LoadCitiesByCountryAsync(countryCode);
            }
        }

        private async void btnSaveChanges_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            var fullName = txtFullName.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var gender = cboGender.SelectedValue.ToString();
            var address = txtAddress.Text.Trim();
            var cityCode = cboCity.SelectedValue?.ToString();

            var ok = await _userService.UpdateAccountAsync(
                UserSession.UserId,
                fullName,
                phone,
                gender,
                cityCode,
                address
            );

            if (!ok)
            {
                MessageBox.Show("Failed to update account.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update UserSession
            UserSession.FullName = fullName;
            UserSession.Phone = phone;

            var parentForm = this.FindForm() as UserDashboard;
            parentForm?.RefreshUserInfoOnNav();

            MessageBox.Show("Account updated.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================== VALIDATION ==================

        private bool ValidateForm()
        {
            // reset lỗi (nếu dùng ErrorProvider)
            errorProvider1?.Clear();

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                isValid = false;
                errorProvider1?.SetError(txtFullName, "Please enter your name");
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                isValid = false;
                errorProvider1?.SetError(txtPhone, "Please enter your phonenumber");
            }
            else if (!txtPhone.Text.All(char.IsDigit) || txtPhone.Text.Length < 8)
            {
                isValid = false;
                errorProvider1?.SetError(txtPhone, "Invalid phonenumber");
            }


            if (cboGender.SelectedItem == null)
            {
                isValid = false;
                errorProvider1?.SetError(cboGender, "Please choose gender");
            }

            if (cboCountry.SelectedValue == null)
            {
                isValid = false;
                errorProvider1?.SetError(cboCountry, "Please choose a country");
            }

            if (cboCity.SelectedValue == null)
            {
                isValid = false;
                errorProvider1?.SetError(cboCity, "Please choose a city");
            }

            return isValid;
        }
    }

}
