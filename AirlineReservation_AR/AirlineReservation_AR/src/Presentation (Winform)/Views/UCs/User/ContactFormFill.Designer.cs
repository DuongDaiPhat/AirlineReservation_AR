namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class ContactFormFill
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel pnlRoot;
        private Label lblTitle;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblPhone;
        private Label lblEmail;

        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtPhoneNumber;

        private ComboBox cboPhoneCode;

        private Label errorFirstName;
        private Label errorLastName;
        private Label errorPhone;
        private Label errorEmail;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlRoot = new Panel();
            lblTitle = new Label();
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
            pnlRoot.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRoot
            // 
            pnlRoot.Controls.Add(lblTitle);
            pnlRoot.Controls.Add(lblFirstName);
            pnlRoot.Controls.Add(txtFirstName);
            pnlRoot.Controls.Add(errorFirstName);
            pnlRoot.Controls.Add(lblLastName);
            pnlRoot.Controls.Add(txtLastName);
            pnlRoot.Controls.Add(errorLastName);
            pnlRoot.Controls.Add(lblPhone);
            pnlRoot.Controls.Add(cboPhoneCode);
            pnlRoot.Controls.Add(txtPhoneNumber);
            pnlRoot.Controls.Add(errorPhone);
            pnlRoot.Controls.Add(lblEmail);
            pnlRoot.Controls.Add(txtEmail);
            pnlRoot.Controls.Add(errorEmail);
            pnlRoot.Dock = DockStyle.Fill;
            pnlRoot.Location = new Point(0, 0);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Padding = new Padding(10);
            pnlRoot.Size = new Size(712, 280);
            pnlRoot.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 5);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(421, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Thông tin liên hệ (nhận vé / phiếu thanh toán)";
            // 
            // lblFirstName
            // 
            lblFirstName.Font = new Font("Segoe UI", 10F);
            lblFirstName.Location = new Point(15, 60);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(100, 23);
            lblFirstName.TabIndex = 1;
            lblFirstName.Text = "Họ*";
            // 
            // txtFirstName
            // 
            txtFirstName.Font = new Font("Segoe UI", 10F);
            txtFirstName.Location = new Point(15, 85);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(270, 25);
            txtFirstName.TabIndex = 2;
            // 
            // errorFirstName
            // 
            errorFirstName.AutoSize = true;
            errorFirstName.Font = new Font("Segoe UI", 9F);
            errorFirstName.ForeColor = Color.Red;
            errorFirstName.Location = new Point(18, 115);
            errorFirstName.Name = "errorFirstName";
            errorFirstName.Size = new Size(115, 15);
            errorFirstName.TabIndex = 3;
            errorFirstName.Text = "Họ là phần bắt buộc";
            errorFirstName.Visible = false;
            // 
            // lblLastName
            // 
            lblLastName.Font = new Font("Segoe UI", 10F);
            lblLastName.Location = new Point(310, 60);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(100, 23);
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Tên đệm và tên*";
            // 
            // txtLastName
            // 
            txtLastName.Font = new Font("Segoe UI", 10F);
            txtLastName.Location = new Point(310, 85);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(270, 25);
            txtLastName.TabIndex = 5;
            // 
            // errorLastName
            // 
            errorLastName.AutoSize = true;
            errorLastName.Font = new Font("Segoe UI", 9F);
            errorLastName.ForeColor = Color.Red;
            errorLastName.Location = new Point(313, 115);
            errorLastName.Name = "errorLastName";
            errorLastName.Size = new Size(137, 15);
            errorLastName.TabIndex = 6;
            errorLastName.Text = "Vui lòng không bỏ trống";
            errorLastName.Visible = false;
            // 
            // lblPhone
            // 
            lblPhone.Font = new Font("Segoe UI", 10F);
            lblPhone.Location = new Point(15, 160);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(100, 23);
            lblPhone.TabIndex = 7;
            lblPhone.Text = "Điện thoại di động*";
            // 
            // cboPhoneCode
            // 
            cboPhoneCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhoneCode.Font = new Font("Segoe UI", 10F);
            cboPhoneCode.Location = new Point(15, 185);
            cboPhoneCode.Name = "cboPhoneCode";
            cboPhoneCode.Size = new Size(70, 25);
            cboPhoneCode.TabIndex = 8;
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Font = new Font("Segoe UI", 10F);
            txtPhoneNumber.Location = new Point(95, 185);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(190, 25);
            txtPhoneNumber.TabIndex = 9;
            // 
            // errorPhone
            // 
            errorPhone.AutoSize = true;
            errorPhone.Font = new Font("Segoe UI", 9F);
            errorPhone.ForeColor = Color.Red;
            errorPhone.Location = new Point(18, 215);
            errorPhone.Name = "errorPhone";
            errorPhone.Size = new Size(153, 15);
            errorPhone.TabIndex = 10;
            errorPhone.Text = "Điện thoại là phần bắt buộc";
            errorPhone.Visible = false;
            // 
            // lblEmail
            // 
            lblEmail.Font = new Font("Segoe UI", 10F);
            lblEmail.Location = new Point(310, 160);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 11;
            lblEmail.Text = "Email*";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(310, 185);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(270, 25);
            txtEmail.TabIndex = 12;
            // 
            // errorEmail
            // 
            errorEmail.AutoSize = true;
            errorEmail.Font = new Font("Segoe UI", 9F);
            errorEmail.ForeColor = Color.Red;
            errorEmail.Location = new Point(313, 215);
            errorEmail.Name = "errorEmail";
            errorEmail.Size = new Size(109, 15);
            errorEmail.TabIndex = 13;
            errorEmail.Text = "Email không hợp lệ";
            errorEmail.Visible = false;
            // 
            // ContactFormFill
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlRoot);
            Name = "ContactFormFill";
            Size = new Size(712, 280);
            pnlRoot.ResumeLayout(false);
            pnlRoot.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
