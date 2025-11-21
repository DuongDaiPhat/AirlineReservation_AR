using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class PassengerFormFill : UserControl
    {
        public string PassengerType { get; set; } = "Adult";   // Adult / Child / Infant
        public int PassengerIndex { get; set; }

        private bool _isInternational = false;

        public string PassengerTitle
        {
            get => lblAdult1.Text;
            set => lblAdult1.Text = value;
        }

        public PassengerFormFill()
        {
            InitializeComponent();
            LoadTitles();
            LoadNationality();
            HookResetEvents();
        }

        // LOAD DATA
        private void LoadTitles()
        {
            cboTitle.Items.AddRange(new string[]
            {
                "Mr", "Mrs", "Miss"
            });
            cboTitle.SelectedIndex = 0;
        }

        private void LoadNationality()
        {
            cboNationality.Items.AddRange(new string[]
            {
                "Vietnam", "Thailand", "Singapore", "Malaysia",
                "United States", "United Kingdom", "Australia", "France",
                "Germany", "Japan", "South Korea"
            });
            cboNationality.SelectedIndex = 0;

            cboCountryOfIssue.Items.AddRange(
            cboNationality.Items.Cast<object>().ToArray()
            );
            cboCountryOfIssue.SelectedIndex = 0;
        }

        // PASSPORT SHOW/HIDE
        public void TogglePassportSection(bool isInternational)
        {
            _isInternational = isInternational;

            lblPassportHeader.Visible = isInternational;
            infoPanel.Visible = isInternational;
            lblPassportNumber.Visible = isInternational;
            txtPassportNumber.Visible = isInternational;
            lblPassportNote.Visible = isInternational;
            lblCountryOfIssue.Visible = isInternational;
            cboCountryOfIssue.Visible = isInternational;
            lblExpiryDate.Visible = isInternational;
            txtExpiryDay.Visible = isInternational;
            txtExpiryMonth.Visible = isInternational;
            txtExpiryYear.Visible = isInternational;
        }

        // DATA EXTRACTOR
        public PassengerDTO GetPassenger()
        {
            return new PassengerDTO
            {
                PassengerType = this.PassengerType,

                Title = cboTitle.Text,
                LastName = txtLastName.Text,
                MiddleName = txtMiddleFirstName.Text,
                FirstName = txtMiddleFirstName.Text,

                DateOfBirth = new DateTime(
                    int.Parse(txtDobYear.Text),
                    int.Parse(txtDobMonth.Text),
                    int.Parse(txtDobDay.Text)
                ),

                PassportNumber = _isInternational ? txtPassportNumber.Text : null,
                Nationality = cboNationality.Text,
                CountryIssue = _isInternational ? cboCountryOfIssue.Text : null,
                PassportExpire = _isInternational
                    ? new DateTime(
                        int.Parse(txtExpiryYear.Text),
                        int.Parse(txtExpiryMonth.Text),
                        int.Parse(txtExpiryDay.Text)
                      )
                    : null
            };
        }


        // VALIDATION
        public bool ValidatePassenger()
        {
            bool ok = true;

            // Reset màu
            ResetError(txtLastName, txtMiddleFirstName, txtMiddleFirstName);

            // 1. Họ
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MarkError(txtLastName);
                ok = false;
            }

            // 2. Tên
            if (string.IsNullOrWhiteSpace(txtMiddleFirstName.Text))
            {
                MarkError(txtMiddleFirstName);
                ok = false;
            }

            // 3. Ngày sinh
            if (!IsValidDate(txtDobDay.Text, txtDobMonth.Text, txtDobYear.Text))
            {
                MarkError(txtDobDay, txtDobMonth, txtDobYear);
                ok = false;
            }
            else
            {
                // Kiểm tra tuổi theo loại hành khách
                var dob = new DateTime(
                    int.Parse(txtDobYear.Text),
                    int.Parse(txtDobMonth.Text),
                    int.Parse(txtDobDay.Text)
                );

                int age = GetAge(dob);
                if (!ValidateAgeByType(age))
                {
                    MarkError(txtDobDay, txtDobMonth, txtDobYear);
                    ok = false;
                }
            }

            // 4. Passport (nếu quốc tế)
            if (_isInternational)
            {
                if (string.IsNullOrWhiteSpace(txtPassportNumber.Text))
                {
                    MarkError(txtPassportNumber);
                    ok = false;
                }

                if (!IsValidDate(txtExpiryDay.Text, txtExpiryMonth.Text, txtExpiryYear.Text))
                {
                    MarkError(txtExpiryDay, txtExpiryMonth, txtExpiryYear);
                    ok = false;
                }
            }

            return ok;
        }

        private bool ValidateAgeByType(int age)
        {
            if (PassengerType == "Adult") return age >= 12;
            if (PassengerType == "Child") return age >= 2 && age < 12;
            if (PassengerType == "Infant") return age < 2;

            return true;
        }

        private int GetAge(DateTime dob)
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }

        private bool IsValidDate(string d, string m, string y)
        {
            if (!int.TryParse(d, out int dd)) return false;
            if (!int.TryParse(m, out int mm)) return false;
            if (!int.TryParse(y, out int yy)) return false;

            try
            {
                var date = new DateTime(yy, mm, dd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ----------------------------------------------------------------
        // ERROR UI EFFECTS
        // ----------------------------------------------------------------
        private void MarkError(params TextBox[] boxes)
        {
            foreach (var txt in boxes)
            {
                txt.BackColor = Color.MistyRose;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void ResetError(params TextBox[] boxes)
        {
            foreach (var txt in boxes)
            {
                txt.BackColor = Color.White;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void HookResetEvents()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.TextChanged += (s, e) =>
                    {
                        txt.BackColor = Color.White;
                    };
                }
            }
        }
    }
}
