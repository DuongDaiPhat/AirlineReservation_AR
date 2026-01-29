using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    public partial class EditBookingCustomerForm : Form
    {
        public string FullName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }

        private Guna2TextBox txtFullName;
        private Guna2TextBox txtPhone;
        private Guna2TextBox txtEmail;
        private Guna2Button btnSave;
        private Guna2Button btnCancel;
        private Guna2BorderlessForm borderlessForm;
        private System.ComponentModel.IContainer components = null;

        public EditBookingCustomerForm(string currentName, string currentPhone, string currentEmail)
        {
            InitializeComponent();
            txtFullName.Text = currentName;
            txtPhone.Text = currentPhone;
            txtEmail.Text = currentEmail;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.borderlessForm = new Guna2BorderlessForm(this.components);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            
            // Borderless Form Config
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.borderlessForm.TransparentWhileDrag = true;
            this.borderlessForm.BorderRadius = 10;

            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(13, 27, 42) };
            var lblTitle = new Label { Text = "Edit Customer Info", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true, Location = new Point(20, 15) };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            // Shadow
            new Guna2ShadowForm(this.components).SetShadowForm(this);

            // Controls
            int startY = 80;
            int gap = 70;

            txtFullName = CreateTextBox("Full Name", 20, startY);
            txtPhone = CreateTextBox("Phone Number", 20, startY + gap);
            txtEmail = CreateTextBox("Email Address", 20, startY + gap * 2);

            this.Controls.Add(txtFullName);
            this.Controls.Add(txtPhone);
            this.Controls.Add(txtEmail);

            // Buttons
            btnCancel = new Guna2Button { Text = "Cancel", Size = new Size(100, 40), Location = new Point(80, 260), FillColor = Color.Gray, BorderRadius = 5 };
            btnSave = new Guna2Button { Text = "Save", Size = new Size(100, 40), Location = new Point(220, 260), FillColor = Color.FromArgb(28, 113, 216), BorderRadius = 5 };
            
            btnCancel.Click += (s, e) => this.Close();
            btnSave.Click += BtnSave_Click;

            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSave);
        }

        private Guna2TextBox CreateTextBox(string placeholder, int x, int y)
        {
            return new Guna2TextBox
            {
                PlaceholderText = placeholder,
                Location = new Point(x, y),
                Size = new Size(360, 40),
                BorderRadius = 5,
                Font = new Font("Segoe UI", 10)
            };
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FullName = txtFullName.Text.Trim();
            Phone = txtPhone.Text.Trim();
            Email = txtEmail.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
