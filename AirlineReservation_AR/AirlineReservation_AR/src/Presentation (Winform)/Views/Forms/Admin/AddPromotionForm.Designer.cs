namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    partial class AddPromotionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private TextBox txtPromoCode;
        private TextBox txtPromoName;
        private TextBox txtDescription;
        private ComboBox cboDiscountType;
        private NumericUpDown numDiscountValue;
        private Label lblDiscountUnit;
        private NumericUpDown numMinAmount;
        private NumericUpDown numMaxDiscount;
        private NumericUpDown numUsageLimit;
        private NumericUpDown numUserUsageLimit;
        private DateTimePicker dtpValidFrom;
        private DateTimePicker dtpValidTo;
        private Button btnSave;
        private Button btnCancel;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Add Promotion Code";
            this.Size = new Size(600, 830);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AutoScroll = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            int yPos = 20;
            int labelX = 30;
            int controlX = 200;
            int controlWidth = 350;
            int spacing = 55;

            // Header Panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(600, 80),
                BackColor = Color.FromArgb(67, 233, 123)
            };

            lblTitle = new Label
            {
                Text = "Create New Promotion",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            yPos = 100;

            // Promo Code
            AddLabel("Promotion Code: *", labelX, yPos);
            txtPromoCode = new TextBox
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                CharacterCasing = CharacterCasing.Upper,
                MaxLength = 20
            };
            this.Controls.Add(txtPromoCode);
            yPos += spacing;

            // Promo Name
            AddLabel("Promotion Name: *", labelX, yPos);
            txtPromoName = new TextBox
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                MaxLength = 100
            };
            this.Controls.Add(txtPromoName);
            yPos += spacing;

            // Description
            AddLabel("Description:", labelX, yPos);
            txtDescription = new TextBox
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 60),
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                MaxLength = 500,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(txtDescription);
            yPos += 75;

            // Discount Type
            AddLabel("Discount Type: *", labelX, yPos);
            cboDiscountType = new ComboBox
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboDiscountType.Items.AddRange(new object[] { "Percent", "Fixed" });
            cboDiscountType.SelectedIndex = 0;
            cboDiscountType.SelectedIndexChanged += CboDiscountType_SelectedIndexChanged;
            this.Controls.Add(cboDiscountType);
            yPos += spacing;

            // Discount Value
            AddLabel("Discount Value: *", labelX, yPos);
            numDiscountValue = new NumericUpDown
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                Maximum = 100,
                Minimum = 1,
                DecimalPlaces = 0
            };
            lblDiscountUnit = new Label
            {
                Text = "%",
                Location = new Point(controlX + controlWidth + 10, yPos + 5),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(67, 233, 123),
                AutoSize = true
            };
            this.Controls.Add(numDiscountValue);
            this.Controls.Add(lblDiscountUnit);
            yPos += spacing;

            // Minimum Amount
            AddLabel("Minimum Amount:", labelX, yPos);
            numMinAmount = new NumericUpDown
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                Maximum = 100000000,
                Minimum = 0,
                DecimalPlaces = 0,
                ThousandsSeparator = true,
                Increment = 100000
            };
            var lblMinUnit = new Label
            {
                Text = "₫",
                Location = new Point(controlX + controlWidth + 10, yPos + 5),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(67, 233, 123),
                AutoSize = true
            };
            this.Controls.Add(numMinAmount);
            this.Controls.Add(lblMinUnit);
            yPos += spacing;

            // Maximum Discount
            AddLabel("Maximum Discount:", labelX, yPos);
            numMaxDiscount = new NumericUpDown
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                Maximum = 10000000,
                Minimum = 0,
                DecimalPlaces = 0,
                ThousandsSeparator = true,
                Increment = 50000
            };
            var lblMaxUnit = new Label
            {
                Text = "₫",
                Location = new Point(controlX + controlWidth + 10, yPos + 5),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(67, 233, 123),
                AutoSize = true
            };
            this.Controls.Add(numMaxDiscount);
            this.Controls.Add(lblMaxUnit);
            yPos += spacing;

            // Usage Limit
            AddLabel("Usage Limit:", labelX, yPos);
            numUsageLimit = new NumericUpDown
            {
                Location = new Point(controlX, yPos),
                Size = new Size(170, 30),
                Font = new Font("Segoe UI", 11),
                Maximum = 100000,
                Minimum = 0,
                DecimalPlaces = 0,
                Value = 100
            };
            var lblUsageUnit = new Label
            {
                Text = "(0 = unlimited)",
                Location = new Point(controlX + 180, yPos + 5),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = true
            };
            this.Controls.Add(numUsageLimit);
            this.Controls.Add(lblUsageUnit);
            yPos += spacing;

            // User Usage Limit
            AddLabel("Limit Per User:", labelX, yPos);
            numUserUsageLimit = new NumericUpDown
            {
                Location = new Point(controlX, yPos),
                Size = new Size(170, 30),
                Font = new Font("Segoe UI", 11),
                Maximum = 100,
                Minimum = 0,
                DecimalPlaces = 0,
                Value = 1
            };
            var lblUserUsageUnit = new Label
            {
                Text = "(0 = unlimited)",
                Location = new Point(controlX + 180, yPos + 5),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = true
            };
            this.Controls.Add(numUserUsageLimit);
            this.Controls.Add(lblUserUsageUnit);
            yPos += spacing;

            // Valid From
            AddLabel("Valid From: *", labelX, yPos);
            dtpValidFrom = new DateTimePicker
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today
            };
            this.Controls.Add(dtpValidFrom);
            yPos += spacing;

            // Valid To
            AddLabel("Valid To: *", labelX, yPos);
            dtpValidTo = new DateTimePicker
            {
                Location = new Point(controlX, yPos),
                Size = new Size(controlWidth, 30),
                Font = new Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today.AddDays(1)
            };
            this.Controls.Add(dtpValidTo);
            yPos += 50;

            // Action Buttons Panel
            var pnlButtons = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(600, 70),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            btnSave = new Button
            {
                Text = "Create Promotion",
                Location = new Point(200, 15),
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(390, 15),
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnCancel });
            this.Controls.Add(pnlButtons);

            this.ResumeLayout();
        }

        #endregion
    }
}