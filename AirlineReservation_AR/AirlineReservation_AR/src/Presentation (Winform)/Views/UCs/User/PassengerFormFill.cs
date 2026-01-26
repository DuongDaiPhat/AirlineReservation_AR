using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
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
                Index = PassengerIndex,
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

            ResetError(
                txtLastName, txtMiddleFirstName,
                txtDobDay, txtDobMonth, txtDobYear,
                txtPassportNumber, txtExpiryDay, txtExpiryMonth, txtExpiryYear
            );

            // ------------------------------
            // 1. Validate LastName
            // ------------------------------
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                !Regex.IsMatch(txtLastName.Text.Trim(), @"^[A-Za-zÀ-ỹ\s]+$"))
            {
                MarkError(txtLastName);
                ok = false;
            }

            // ------------------------------
            // 2. Validate First+Middle Name
            // ------------------------------
            if (string.IsNullOrWhiteSpace(txtMiddleFirstName.Text) ||
                !Regex.IsMatch(txtMiddleFirstName.Text.Trim(), @"^[A-Za-zÀ-ỹ\s]+$"))
            {
                MarkError(txtMiddleFirstName);
                ok = false;
            }

            // ------------------------------
            // 3. Validate Date of Birth
            // ------------------------------
            if (!TryParseDate(txtDobDay, txtDobMonth, txtDobYear, out DateTime dob))
            {
                MarkError(txtDobDay, txtDobMonth, txtDobYear);
                ok = false;
            }
            else
            {
                int age = GetAge(dob);
                if (!ValidateAgeByType(age))
                {
                    MarkError(txtDobDay, txtDobMonth, txtDobYear);
                    ok = false;
                }
            }

            // ------------------------------
            // 4. Validate Passport (International flights only)
            // ------------------------------
            if (_isInternational)
            {
                // Passport Number
                string passport = txtPassportNumber.Text.Trim().ToUpper();
                if (!Regex.IsMatch(passport, @"^[A-Z0-9]{6,12}$"))
                {
                    MarkError(txtPassportNumber);
                    ok = false;
                }

                // Passport Expiry Date
                if (!TryParseDate(txtExpiryDay, txtExpiryMonth, txtExpiryYear, out DateTime exp))
                {
                    MarkError(txtExpiryDay, txtExpiryMonth, txtExpiryYear);
                    ok = false;
                }
                else
                {
                    if (exp <= DateTime.Today)
                    {
                        MarkError(txtExpiryDay, txtExpiryMonth, txtExpiryYear);
                        ok = false;
                    }
                }
            }

            return ok;
        }

        private bool TryParseDate(TextBox d, TextBox m, TextBox y, out DateTime dt)
        {
            dt = DateTime.MinValue;

            if (!int.TryParse(d.Text, out int dd)) return false;
            if (!int.TryParse(m.Text, out int mm)) return false;
            if (!int.TryParse(y.Text, out int yy)) return false;

            if (yy < 1900 || yy > 2100) return false;

            return DateTime.TryParse($"{yy}-{mm}-{dd}", out dt);
        }

        private bool ValidateAgeByType(int age)
        {
            return PassengerType switch
            {
                "Adult" => age >= 12,
                "Child" => age >= 2 && age < 12,
                "Infant" => age < 2,
                _ => true
            };
        }

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
