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
using System.Text.RegularExpressions;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    public partial class ContactFormFill : UserControl
    {

        public ContactFormFill()
        {
            InitializeComponent();
            LoadPhoneCodes();
            HookResetEvents();
            txtPhoneNumber.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            };
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

            // 1. First name – không để trống và không chứa số
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                !Regex.IsMatch(txtFirstName.Text.Trim(), @"^[A-Za-zÀ-ỹ\s]+$"))
            {
                MarkError(txtFirstName, errorFirstName);
                ok = false;
            }

            // 2. Last name – không để trống và không chứa số
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                !Regex.IsMatch(txtLastName.Text.Trim(), @"^[A-Za-zÀ-ỹ\s]+$"))
            {
                MarkError(txtLastName, errorLastName);
                ok = false;
            }

            // 3. Phone number
            string phone = txtPhoneNumber.Text.Trim();
            if (!Regex.IsMatch(phone, @"^[0-9]{9,15}$"))
            {
                errorPhone.Text = "Số điện thoại chỉ được chứa số (9–15 ký tự)";
                MarkError(txtPhoneNumber, errorPhone);
                ok = false;
            }

            // 4. Phone number – validate chuẩn Việt Nam
            if (cboPhoneCode.Text == "+84" &&
                !Regex.IsMatch(phone, @"^(03|05|07|08|09)[0-9]{8}$"))
            {
                errorPhone.Text = "Số điện thoại Việt Nam không hợp lệ";
                MarkError(txtPhoneNumber, errorPhone);
                ok = false;
            }

            // 5. Email
            if (!Regex.IsMatch(txtEmail.Text.Trim(),
                 @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
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
