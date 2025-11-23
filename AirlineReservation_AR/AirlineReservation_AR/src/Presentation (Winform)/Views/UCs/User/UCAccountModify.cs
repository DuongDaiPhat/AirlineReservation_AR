using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.Domain.DTOs;
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
        private UserDTO _user;
        public event EventHandler AccountUpdated;

        private bool _isLoading = false;
        public UCAccountModify(UserDTO user)
        {
            InitializeComponent();

            this.Load += UCAccountModify_Load;
            _user = user;

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
            if (_user.UserId == Guid.Empty)
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
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '+' && txtPhone.SelectionStart == 0 && !txtPhone.Text.Contains("+"))
                return;

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
            var user = await _userService.GetByIdAsync(_user.UserId);
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
                    cboCountry.SelectedValue = city.CountryCode;
                    await LoadCitiesByCountryAsync(city.CountryCode);
                    cboCity.SelectedValue = user.CityCode;
                }
            }
            else
            {
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
            var gender = cboGender.SelectedValue?.ToString();
            var address = txtAddress.Text.Trim();
            var cityCode = cboCity.SelectedValue?.ToString();
            var user = DIContainer.CurrentUser;
            var ok = await _userService.UpdateAccountAsync(
                _user.UserId,
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

            _user.UserName = fullName;
            _user.Phone = phone;

            AccountUpdated?.Invoke(this, EventArgs.Empty);
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

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }

}
