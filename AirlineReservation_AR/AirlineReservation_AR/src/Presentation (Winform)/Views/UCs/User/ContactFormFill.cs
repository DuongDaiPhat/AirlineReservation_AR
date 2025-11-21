using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineReservation_AR.src.Domain.DTOs;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class ContactFormFill : UserControl
    {
        public ContactFormFill()
        {
            InitializeComponent();
            LoadPhoneCodes();
            HookResetEvents();
        }
        private void LoadPhoneCodes()
        {
            cboPhoneCode.Items.AddRange(new object[]
            {
        "+84", "+66", "+65", "+60", "+1", "+44", "+81", "+82"
            });
            cboPhoneCode.SelectedIndex = 0;
        }

        // ------------------------------------------------------
        // DTO extractor
        // ------------------------------------------------------
        public ContactInfoDTO GetContact()
        {
            return new ContactInfoDTO
            {
                FirstName = txtFirstName.Text.Trim(),
                MiddleAndLastName = txtLastName.Text.Trim(),
                PhoneCode = cboPhoneCode.Text,
                Phone = txtPhoneNumber.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
        }

        // ------------------------------------------------------
        // VALIDATION
        // ------------------------------------------------------
        public bool ValidateContact()
        {
            bool ok = true;

            ResetError(txtFirstName, txtLastName, txtPhoneNumber, txtEmail);

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MarkError(txtFirstName, errorFirstName);
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MarkError(txtLastName, errorLastName);
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MarkError(txtPhoneNumber, errorPhone);
                ok = false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MarkError(txtEmail, errorEmail);
                ok = false;
            }

            return ok;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // ------------------------------------------------------
        // UI Effects
        // ------------------------------------------------------
        private void MarkError(TextBox box, Label errorLabel)
        {
            box.BackColor = Color.MistyRose;
            errorLabel.Visible = true;
        }

        private void ResetError(params TextBox[] boxes)
        {
            foreach (var box in boxes)
            {
                box.BackColor = Color.White;
            }

            errorFirstName.Visible = false;
            errorLastName.Visible = false;
            errorPhone.Visible = false;
            errorEmail.Visible = false;
        }

        private void HookResetEvents()
        {
            txtFirstName.TextChanged += (s, e) => txtFirstName.BackColor = Color.White;
            txtLastName.TextChanged += (s, e) => txtLastName.BackColor = Color.White;
            txtEmail.TextChanged += (s, e) => txtEmail.BackColor = Color.White;
            txtPhoneNumber.TextChanged += (s, e) => txtPhoneNumber.BackColor = Color.White;
        }
    }
}
