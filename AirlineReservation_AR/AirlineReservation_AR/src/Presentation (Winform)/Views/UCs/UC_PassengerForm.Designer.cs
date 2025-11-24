namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs
{
    partial class UC_PassengerForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            mainPanel = new Panel();
            txtExpiryYear = new TextBox();
            txtExpiryMonth = new TextBox();
            txtExpiryDay = new TextBox();
            lblExpiryDate = new Label();
            lblCountryOfIssue = new Label();
            lblPassportNote = new Label();
            txtPassportNumber = new TextBox();
            lblPassportNumber = new Label();
            infoPanel = new Panel();
            lnkLearnMore = new LinkLabel();
            lblPassportInfo = new Label();
            lblPassportHeader = new Label();
            lblNationality = new Label();
            lblAgeNote = new Label();
            txtDobYear = new TextBox();
            txtDobMonth = new TextBox();
            txtDobDay = new TextBox();
            lblDob = new Label();
            lblMiddleFirstNameNote = new Label();
            txtMiddleFirstName = new TextBox();
            lblMiddleFirstName = new Label();
            lblLastNameNote = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            lblTitle = new Label();
            warningPanel = new Panel();
            lnkNameGuideline = new LinkLabel();
            lblWarningText = new Label();
            lblWarning = new Label();
            lblWarningIcon = new Label();
            lblAdult1 = new Label();
            cboNationality = new Guna.UI2.WinForms.Guna2ComboBox();
            cboTitle = new Guna.UI2.WinForms.Guna2ComboBox();
            cboCountryOfIssue = new Guna.UI2.WinForms.Guna2ComboBox();
            mainPanel.SuspendLayout();
            infoPanel.SuspendLayout();
            warningPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.BackColor = Color.White;
            mainPanel.Controls.Add(cboCountryOfIssue);
            mainPanel.Controls.Add(cboTitle);
            mainPanel.Controls.Add(cboNationality);
            mainPanel.Controls.Add(txtExpiryYear);
            mainPanel.Controls.Add(txtExpiryMonth);
            mainPanel.Controls.Add(txtExpiryDay);
            mainPanel.Controls.Add(lblExpiryDate);
            mainPanel.Controls.Add(lblCountryOfIssue);
            mainPanel.Controls.Add(lblPassportNote);
            mainPanel.Controls.Add(txtPassportNumber);
            mainPanel.Controls.Add(lblPassportNumber);
            mainPanel.Controls.Add(infoPanel);
            mainPanel.Controls.Add(lblPassportHeader);
            mainPanel.Controls.Add(lblNationality);
            mainPanel.Controls.Add(lblAgeNote);
            mainPanel.Controls.Add(txtDobYear);
            mainPanel.Controls.Add(txtDobMonth);
            mainPanel.Controls.Add(txtDobDay);
            mainPanel.Controls.Add(lblDob);
            mainPanel.Controls.Add(lblMiddleFirstNameNote);
            mainPanel.Controls.Add(txtMiddleFirstName);
            mainPanel.Controls.Add(lblMiddleFirstName);
            mainPanel.Controls.Add(lblLastNameNote);
            mainPanel.Controls.Add(txtLastName);
            mainPanel.Controls.Add(lblLastName);
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(warningPanel);
            mainPanel.Controls.Add(lblAdult1);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(980, 760);
            mainPanel.TabIndex = 0;
            // 
            // txtExpiryYear
            // 
            txtExpiryYear.Font = new Font("Segoe UI", 10F);
            txtExpiryYear.ForeColor = Color.Gray;
            txtExpiryYear.Location = new Point(516, 705);
            txtExpiryYear.Multiline = true;
            txtExpiryYear.Name = "txtExpiryYear";
            txtExpiryYear.Size = new Size(90, 30);
            txtExpiryYear.TabIndex = 27;
            txtExpiryYear.Text = "YYYY";
            // 
            // txtExpiryMonth
            // 
            txtExpiryMonth.Font = new Font("Segoe UI", 10F);
            txtExpiryMonth.ForeColor = Color.Gray;
            txtExpiryMonth.Location = new Point(421, 705);
            txtExpiryMonth.Multiline = true;
            txtExpiryMonth.Name = "txtExpiryMonth";
            txtExpiryMonth.Size = new Size(90, 30);
            txtExpiryMonth.TabIndex = 26;
            txtExpiryMonth.Text = "MMMM";
            // 
            // txtExpiryDay
            // 
            txtExpiryDay.Font = new Font("Segoe UI", 10F);
            txtExpiryDay.ForeColor = Color.Gray;
            txtExpiryDay.Location = new Point(326, 705);
            txtExpiryDay.Multiline = true;
            txtExpiryDay.Name = "txtExpiryDay";
            txtExpiryDay.Size = new Size(90, 30);
            txtExpiryDay.TabIndex = 25;
            txtExpiryDay.Text = "DD";
            // 
            // lblExpiryDate
            // 
            lblExpiryDate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblExpiryDate.Location = new Point(326, 680);
            lblExpiryDate.Name = "lblExpiryDate";
            lblExpiryDate.Size = new Size(280, 20);
            lblExpiryDate.TabIndex = 24;
            lblExpiryDate.Text = "Passport Expiry Date*";
            // 
            // lblCountryOfIssue
            // 
            lblCountryOfIssue.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCountryOfIssue.Location = new Point(16, 680);
            lblCountryOfIssue.Name = "lblCountryOfIssue";
            lblCountryOfIssue.Size = new Size(280, 20);
            lblCountryOfIssue.TabIndex = 22;
            lblCountryOfIssue.Text = "Country of Issue*";
            // 
            // lblPassportNote
            // 
            lblPassportNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassportNote.ForeColor = Color.Gray;
            lblPassportNote.Location = new Point(16, 615);
            lblPassportNote.Name = "lblPassportNote";
            lblPassportNote.Size = new Size(416, 50);
            lblPassportNote.TabIndex = 21;
            lblPassportNote.Text = "For child or infant passengers, please enter the guardian's ID of the\r\nperson traveling with them. (Please ensure you enter numbers only in\r\nthis field)";
            // 
            // txtPassportNumber
            // 
            txtPassportNumber.Font = new Font("Segoe UI", 10F);
            txtPassportNumber.Location = new Point(16, 580);
            txtPassportNumber.Multiline = true;
            txtPassportNumber.Name = "txtPassportNumber";
            txtPassportNumber.Size = new Size(416, 30);
            txtPassportNumber.TabIndex = 20;
            // 
            // lblPassportNumber
            // 
            lblPassportNumber.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassportNumber.Location = new Point(16, 555);
            lblPassportNumber.Name = "lblPassportNumber";
            lblPassportNumber.Size = new Size(400, 20);
            lblPassportNumber.TabIndex = 19;
            lblPassportNumber.Text = "Passport Number*";
            // 
            // infoPanel
            // 
            infoPanel.BackColor = Color.FromArgb(230, 244, 255);
            infoPanel.BorderStyle = BorderStyle.FixedSingle;
            infoPanel.Controls.Add(lnkLearnMore);
            infoPanel.Controls.Add(lblPassportInfo);
            infoPanel.Location = new Point(16, 475);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new Size(940, 60);
            infoPanel.TabIndex = 18;
            // 
            // lnkLearnMore
            // 
            lnkLearnMore.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkLearnMore.Location = new Point(10, 35);
            lnkLearnMore.Name = "lnkLearnMore";
            lnkLearnMore.Size = new Size(100, 20);
            lnkLearnMore.TabIndex = 1;
            lnkLearnMore.TabStop = true;
            lnkLearnMore.Text = "Learn more";
            lnkLearnMore.LinkClicked += lnkLearnMore_LinkClicked;
            // 
            // lblPassportInfo
            // 
            lblPassportInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassportInfo.Location = new Point(10, 10);
            lblPassportInfo.Name = "lblPassportInfo";
            lblPassportInfo.Size = new Size(880, 20);
            lblPassportInfo.TabIndex = 0;
            lblPassportInfo.Text = "If this passenger doesn't have a passport yet or passport is expiring, you can still proceed with this booking.";
            // 
            // lblPassportHeader
            // 
            lblPassportHeader.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPassportHeader.Location = new Point(16, 442);
            lblPassportHeader.Name = "lblPassportHeader";
            lblPassportHeader.Size = new Size(900, 30);
            lblPassportHeader.TabIndex = 17;
            lblPassportHeader.Text = "Passport Information";
            // 
            // lblNationality
            // 
            lblNationality.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNationality.Location = new Point(326, 331);
            lblNationality.Name = "lblNationality";
            lblNationality.Size = new Size(280, 20);
            lblNationality.TabIndex = 15;
            lblNationality.Text = "Nationality*";
            // 
            // lblAgeNote
            // 
            lblAgeNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAgeNote.ForeColor = Color.Gray;
            lblAgeNote.Location = new Point(16, 391);
            lblAgeNote.Name = "lblAgeNote";
            lblAgeNote.Size = new Size(280, 20);
            lblAgeNote.TabIndex = 14;
            lblAgeNote.Text = "Adult Passenger (Age 12 and older)";
            // 
            // txtDobYear
            // 
            txtDobYear.Font = new Font("Segoe UI", 10F);
            txtDobYear.ForeColor = Color.Gray;
            txtDobYear.Location = new Point(206, 356);
            txtDobYear.Multiline = true;
            txtDobYear.Name = "txtDobYear";
            txtDobYear.Size = new Size(90, 30);
            txtDobYear.TabIndex = 13;
            txtDobYear.Text = "YYYY";
            // 
            // txtDobMonth
            // 
            txtDobMonth.Font = new Font("Segoe UI", 10F);
            txtDobMonth.ForeColor = Color.Gray;
            txtDobMonth.Location = new Point(111, 356);
            txtDobMonth.Multiline = true;
            txtDobMonth.Name = "txtDobMonth";
            txtDobMonth.Size = new Size(90, 30);
            txtDobMonth.TabIndex = 12;
            txtDobMonth.Text = "MMMM";
            // 
            // txtDobDay
            // 
            txtDobDay.Font = new Font("Segoe UI", 10F);
            txtDobDay.ForeColor = Color.Gray;
            txtDobDay.Location = new Point(16, 356);
            txtDobDay.Multiline = true;
            txtDobDay.Name = "txtDobDay";
            txtDobDay.Size = new Size(90, 30);
            txtDobDay.TabIndex = 11;
            txtDobDay.Text = "DD";
            // 
            // lblDob
            // 
            lblDob.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDob.Location = new Point(16, 331);
            lblDob.Name = "lblDob";
            lblDob.Size = new Size(280, 20);
            lblDob.TabIndex = 10;
            lblDob.Text = "Date of Birth*";
            // 
            // lblMiddleFirstNameNote
            // 
            lblMiddleFirstNameNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMiddleFirstNameNote.ForeColor = Color.Gray;
            lblMiddleFirstNameNote.Location = new Point(326, 292);
            lblMiddleFirstNameNote.Name = "lblMiddleFirstNameNote";
            lblMiddleFirstNameNote.Size = new Size(280, 20);
            lblMiddleFirstNameNote.TabIndex = 9;
            lblMiddleFirstNameNote.Text = "(without title and punctuation)";
            // 
            // txtMiddleFirstName
            // 
            txtMiddleFirstName.Font = new Font("Segoe UI", 10F);
            txtMiddleFirstName.Location = new Point(326, 257);
            txtMiddleFirstName.Multiline = true;
            txtMiddleFirstName.Name = "txtMiddleFirstName";
            txtMiddleFirstName.Size = new Size(280, 30);
            txtMiddleFirstName.TabIndex = 8;
            // 
            // lblMiddleFirstName
            // 
            lblMiddleFirstName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMiddleFirstName.Location = new Point(326, 232);
            lblMiddleFirstName.Name = "lblMiddleFirstName";
            lblMiddleFirstName.Size = new Size(280, 20);
            lblMiddleFirstName.TabIndex = 7;
            lblMiddleFirstName.Text = "Middle & First Name*";
            // 
            // lblLastNameNote
            // 
            lblLastNameNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastNameNote.ForeColor = Color.Gray;
            lblLastNameNote.Location = new Point(16, 292);
            lblLastNameNote.Name = "lblLastNameNote";
            lblLastNameNote.Size = new Size(280, 20);
            lblLastNameNote.TabIndex = 6;
            lblLastNameNote.Text = "(without title and punctuation)";
            // 
            // txtLastName
            // 
            txtLastName.Font = new Font("Segoe UI", 10F);
            txtLastName.Location = new Point(16, 257);
            txtLastName.Multiline = true;
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(280, 30);
            txtLastName.TabIndex = 5;
            // 
            // lblLastName
            // 
            lblLastName.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastName.Location = new Point(16, 232);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(280, 20);
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Last Name*";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(16, 163);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 20);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Title*";
            // 
            // warningPanel
            // 
            warningPanel.BackColor = Color.FromArgb(255, 248, 220);
            warningPanel.BorderStyle = BorderStyle.FixedSingle;
            warningPanel.Controls.Add(lnkNameGuideline);
            warningPanel.Controls.Add(lblWarningText);
            warningPanel.Controls.Add(lblWarning);
            warningPanel.Controls.Add(lblWarningIcon);
            warningPanel.Location = new Point(16, 45);
            warningPanel.Name = "warningPanel";
            warningPanel.Size = new Size(940, 100);
            warningPanel.TabIndex = 1;
            // 
            // lnkNameGuideline
            // 
            lnkNameGuideline.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkNameGuideline.Location = new Point(45, 70);
            lnkNameGuideline.Name = "lnkNameGuideline";
            lnkNameGuideline.Size = new Size(150, 20);
            lnkNameGuideline.TabIndex = 3;
            lnkNameGuideline.TabStop = true;
            lnkNameGuideline.Text = "See name guideline";
            lnkNameGuideline.LinkClicked += lnkNameGuideline_LinkClicked;
            // 
            // lblWarningText
            // 
            lblWarningText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWarningText.Location = new Point(45, 35);
            lblWarningText.Name = "lblWarningText";
            lblWarningText.Size = new Size(854, 35);
            lblWarningText.TabIndex = 2;
            lblWarningText.Text = "Enter your name exactly as in your passport. Incorrect spelling and wrong ordered names may result in denied boarding or name change fees.";
            // 
            // lblWarning
            // 
            lblWarning.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWarning.Location = new Point(45, 10);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(840, 20);
            lblWarning.TabIndex = 1;
            lblWarning.Text = "Please pay attention for the following:";
            // 
            // lblWarningIcon
            // 
            lblWarningIcon.Font = new Font("Segoe UI", 12F);
            lblWarningIcon.Location = new Point(10, 10);
            lblWarningIcon.Name = "lblWarningIcon";
            lblWarningIcon.Size = new Size(30, 30);
            lblWarningIcon.TabIndex = 0;
            lblWarningIcon.Text = "⚠️";
            // 
            // lblAdult1
            // 
            lblAdult1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAdult1.Location = new Point(16, 9);
            lblAdult1.Name = "lblAdult1";
            lblAdult1.Size = new Size(900, 30);
            lblAdult1.TabIndex = 0;
            lblAdult1.Text = "Adult 1";
            // 
            // cboNationality
            // 
            cboNationality.BackColor = Color.Transparent;
            cboNationality.CustomizableEdges = customizableEdges5;
            cboNationality.DrawMode = DrawMode.OwnerDrawFixed;
            cboNationality.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNationality.FocusedColor = Color.FromArgb(94, 148, 255);
            cboNationality.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboNationality.Font = new Font("Segoe UI", 10F);
            cboNationality.ForeColor = Color.FromArgb(68, 88, 112);
            cboNationality.ItemHeight = 25;
            cboNationality.Location = new Point(326, 356);
            cboNationality.Name = "cboNationality";
            cboNationality.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cboNationality.Size = new Size(280, 31);
            cboNationality.TabIndex = 28;
            // 
            // cboTitle
            // 
            cboTitle.BackColor = Color.Transparent;
            cboTitle.CustomizableEdges = customizableEdges3;
            cboTitle.DrawMode = DrawMode.OwnerDrawFixed;
            cboTitle.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTitle.FocusedColor = Color.FromArgb(94, 148, 255);
            cboTitle.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboTitle.Font = new Font("Segoe UI", 10F);
            cboTitle.ForeColor = Color.FromArgb(68, 88, 112);
            cboTitle.ItemHeight = 25;
            cboTitle.Location = new Point(16, 186);
            cboTitle.Name = "cboTitle";
            cboTitle.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cboTitle.Size = new Size(200, 31);
            cboTitle.TabIndex = 29;
            // 
            // cboCountryOfIssue
            // 
            cboCountryOfIssue.BackColor = Color.Transparent;
            cboCountryOfIssue.CustomizableEdges = customizableEdges1;
            cboCountryOfIssue.DrawMode = DrawMode.OwnerDrawFixed;
            cboCountryOfIssue.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCountryOfIssue.FocusedColor = Color.FromArgb(94, 148, 255);
            cboCountryOfIssue.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboCountryOfIssue.Font = new Font("Segoe UI", 10F);
            cboCountryOfIssue.ForeColor = Color.FromArgb(68, 88, 112);
            cboCountryOfIssue.ItemHeight = 25;
            cboCountryOfIssue.Location = new Point(16, 705);
            cboCountryOfIssue.Name = "cboCountryOfIssue";
            cboCountryOfIssue.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cboCountryOfIssue.Size = new Size(280, 31);
            cboCountryOfIssue.TabIndex = 30;
            // 
            // UC_PassengerForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(mainPanel);
            Name = "UC_PassengerForm";
            Size = new Size(980, 760);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            infoPanel.ResumeLayout(false);
            warningPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label lblAdult1;
        private System.Windows.Forms.Panel warningPanel;
        private System.Windows.Forms.Label lblWarningIcon;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblWarningText;
        private System.Windows.Forms.LinkLabel lnkNameGuideline;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblLastNameNote;
        private System.Windows.Forms.Label lblMiddleFirstName;
        private System.Windows.Forms.TextBox txtMiddleFirstName;
        private System.Windows.Forms.Label lblMiddleFirstNameNote;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.TextBox txtDobDay;
        private System.Windows.Forms.TextBox txtDobMonth;
        private System.Windows.Forms.TextBox txtDobYear;
        private System.Windows.Forms.Label lblAgeNote;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.Label lblPassportHeader;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label lblPassportInfo;
        private System.Windows.Forms.LinkLabel lnkLearnMore;
        private System.Windows.Forms.Label lblPassportNumber;
        private System.Windows.Forms.TextBox txtPassportNumber;
        private System.Windows.Forms.Label lblPassportNote;
        private System.Windows.Forms.Label lblCountryOfIssue;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.TextBox txtExpiryDay;
        private System.Windows.Forms.TextBox txtExpiryMonth;
        private System.Windows.Forms.TextBox txtExpiryYear;
        private Guna.UI2.WinForms.Guna2ComboBox cboTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cboNationality;
        private Guna.UI2.WinForms.Guna2ComboBox cboCountryOfIssue;
    }
}
