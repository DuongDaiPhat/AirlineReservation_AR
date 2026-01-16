namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class ContactFormFill
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlRoot;
        private Panel pnlHeader;
        private PictureBox picUserIcon;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlForm;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label errorFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label errorLastName;
        private Label lblPhone;
        private ComboBox cboPhoneCode;
        private TextBox txtPhoneNumber;
        private Label errorPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label errorEmail;
        private Label lblNote;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlRoot = new Panel();
            pnlForm = new Panel();
            lblNote = new Label();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            errorFirstName = new Label();
            lblLastName = new Label();
            txtLastName = new TextBox();
            errorLastName = new Label();
            lblPhone = new Label();
            cboPhoneCode = new ComboBox();
            txtPhoneNumber = new TextBox();
            errorPhone = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            errorEmail = new Label();
            pnlHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            picUserIcon = new PictureBox();
            pnlRoot.SuspendLayout();
            pnlForm.SuspendLayout();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUserIcon).BeginInit();
            SuspendLayout();
            // 
            // pnlRoot
            // 
            pnlRoot.BackColor = Color.White;
            pnlRoot.Controls.Add(pnlForm);
            pnlRoot.Controls.Add(pnlHeader);
            pnlRoot.Dock = DockStyle.Fill;
            pnlRoot.Location = new Point(0, 0);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Padding = new Padding(20);
            pnlRoot.Size = new Size(1073, 237);
            pnlRoot.TabIndex = 0;
            // 
            // pnlForm
            // 
            pnlForm.Controls.Add(lblNote);
            pnlForm.Controls.Add(lblFirstName);
            pnlForm.Controls.Add(txtFirstName);
            pnlForm.Controls.Add(errorFirstName);
            pnlForm.Controls.Add(lblLastName);
            pnlForm.Controls.Add(txtLastName);
            pnlForm.Controls.Add(errorLastName);
            pnlForm.Controls.Add(lblPhone);
            pnlForm.Controls.Add(cboPhoneCode);
            pnlForm.Controls.Add(txtPhoneNumber);
            pnlForm.Controls.Add(errorPhone);
            pnlForm.Controls.Add(lblEmail);
            pnlForm.Controls.Add(txtEmail);
            pnlForm.Controls.Add(errorEmail);
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Location = new Point(20, 80);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(1033, 137);
            pnlForm.TabIndex = 1;
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.Font = new Font("Segoe UI", 8.5F);
            lblNote.ForeColor = Color.Gray;
            lblNote.Location = new Point(0, 90);
            lblNote.Name = "lblNote";
            lblNote.Size = new Size(425, 15);
            lblNote.TabIndex = 13;
            lblNote.Text = "* Please fill in all information to receive e-ticket and flight information via email";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFirstName.ForeColor = Color.FromArgb(51, 51, 51);
            lblFirstName.Location = new Point(0, 10);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(77, 17);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "Last name*";
            // 
            // txtFirstName
            // 
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtFirstName.Font = new Font("Segoe UI", 11F);
            txtFirstName.Location = new Point(0, 30);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(250, 27);
            txtFirstName.TabIndex = 1;
            // 
            // errorFirstName
            // 
            errorFirstName.AutoSize = true;
            errorFirstName.Font = new Font("Segoe UI", 8.5F);
            errorFirstName.ForeColor = Color.Red;
            errorFirstName.Location = new Point(0, 65);
            errorFirstName.Name = "errorFirstName";
            errorFirstName.Size = new Size(191, 15);
            errorFirstName.TabIndex = 2;
            errorFirstName.Text = "Sorry, please enter letters only (a-z)";
            errorFirstName.Visible = false;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLastName.ForeColor = Color.FromArgb(51, 51, 51);
            lblLastName.Location = new Point(270, 10);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(153, 17);
            lblLastName.TabIndex = 3;
            lblLastName.Text = "Middle and First name*";
            // 
            // txtLastName
            // 
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Font = new Font("Segoe UI", 11F);
            txtLastName.Location = new Point(270, 30);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(250, 27);
            txtLastName.TabIndex = 4;
            // 
            // errorLastName
            // 
            errorLastName.AutoSize = true;
            errorLastName.Font = new Font("Segoe UI", 8.5F);
            errorLastName.ForeColor = Color.Red;
            errorLastName.Location = new Point(270, 65);
            errorLastName.Name = "errorLastName";
            errorLastName.Size = new Size(191, 15);
            errorLastName.TabIndex = 5;
            errorLastName.Text = "Sorry, please enter letters only (a-z)";
            errorLastName.Visible = false;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(51, 51, 51);
            lblPhone.Location = new Point(540, 10);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(107, 17);
            lblPhone.TabIndex = 6;
            lblPhone.Text = "Phone Number*";
            // 
            // cboPhoneCode
            // 
            cboPhoneCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhoneCode.Font = new Font("Segoe UI", 11F);
            cboPhoneCode.FormattingEnabled = true;
            cboPhoneCode.Location = new Point(540, 30);
            cboPhoneCode.Name = "cboPhoneCode";
            cboPhoneCode.Size = new Size(80, 28);
            cboPhoneCode.TabIndex = 7;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
            txtPhoneNumber.Font = new Font("Segoe UI", 11F);
            txtPhoneNumber.Location = new Point(630, 30);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(170, 27);
            txtPhoneNumber.TabIndex = 8;
            // 
            // errorPhone
            // 
            errorPhone.AutoSize = true;
            errorPhone.Font = new Font("Segoe UI", 8.5F);
            errorPhone.ForeColor = Color.Red;
            errorPhone.Location = new Point(540, 65);
            errorPhone.Name = "errorPhone";
            errorPhone.Size = new Size(158, 15);
            errorPhone.TabIndex = 9;
            errorPhone.Text = "Phone number is mandatory";
            errorPhone.Visible = false;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(51, 51, 51);
            lblEmail.Location = new Point(820, 10);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(48, 17);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Email*";
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(820, 30);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 11;
            // 
            // errorEmail
            // 
            errorEmail.AutoSize = true;
            errorEmail.Font = new Font("Segoe UI", 8.5F);
            errorEmail.ForeColor = Color.Red;
            errorEmail.Location = new Point(820, 65);
            errorEmail.Name = "errorEmail";
            errorEmail.Size = new Size(135, 15);
            errorEmail.TabIndex = 12;
            errorEmail.Text = "Ex: email@example.com";
            errorEmail.Visible = false;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(picUserIcon);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(20, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1033, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(60, 35);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(80, 15);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Sign in as Kha";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(51, 51, 51);
            lblTitle.Location = new Point(60, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(214, 30);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Contact Infomation";
            // 
            // picUserIcon
            // 
            picUserIcon.BackColor = Color.FromArgb(0, 149, 238);
            picUserIcon.Location = new Point(0, 5);
            picUserIcon.Name = "picUserIcon";
            picUserIcon.Size = new Size(50, 50);
            picUserIcon.TabIndex = 0;
            picUserIcon.TabStop = false;
            picUserIcon.Paint += picUserIcon_Paint;
            // 
            // ContactFormFill
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlRoot);
            Name = "ContactFormFill";
            Size = new Size(1073, 237);
            pnlRoot.ResumeLayout(false);
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUserIcon).EndInit();
            ResumeLayout(false);
        }
    }
}