namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class FlightDetailCard
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightDetailCard));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            Prices = new Guna.UI2.WinForms.Guna2HtmlLabel();
            Airline = new Guna.UI2.WinForms.Guna2HtmlLabel();
            hours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ToDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ToCode = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ToCity = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ToHours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            pictureBox1 = new PictureBox();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            FromDate = new Guna.UI2.WinForms.Guna2HtmlLabel();
            FromCode = new Guna.UI2.WinForms.Guna2HtmlLabel();
            FromCity = new Guna.UI2.WinForms.Guna2HtmlLabel();
            FromHours = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = Color.DimGray;
            guna2Panel1.BorderRadius = 20;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(Prices);
            guna2Panel1.Controls.Add(Airline);
            guna2Panel1.Controls.Add(hours);
            guna2Panel1.Controls.Add(ToDate);
            guna2Panel1.Controls.Add(ToCode);
            guna2Panel1.Controls.Add(ToCity);
            guna2Panel1.Controls.Add(ToHours);
            guna2Panel1.Controls.Add(guna2PictureBox2);
            guna2Panel1.Controls.Add(pictureBox1);
            guna2Panel1.Controls.Add(guna2PictureBox1);
            guna2Panel1.Controls.Add(FromDate);
            guna2Panel1.Controls.Add(FromCode);
            guna2Panel1.Controls.Add(FromCity);
            guna2Panel1.Controls.Add(FromHours);
            guna2Panel1.CustomizableEdges = customizableEdges5;
            guna2Panel1.FillColor = Color.WhiteSmoke;
            guna2Panel1.Location = new Point(3, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel1.Size = new Size(710, 213);
            guna2Panel1.TabIndex = 0;
            // 
            // Prices
            // 
            Prices.AutoSize = false;
            Prices.BackColor = Color.Transparent;
            Prices.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Prices.Location = new Point(486, 167);
            Prices.Name = "Prices";
            Prices.Size = new Size(189, 27);
            Prices.TabIndex = 30;
            Prices.Text = "2.450.000 ₫";
            Prices.TextAlignment = ContentAlignment.MiddleRight;
            // 
            // Airline
            // 
            Airline.BackColor = Color.Transparent;
            Airline.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Airline.ForeColor = SystemColors.ControlDarkDark;
            Airline.Location = new Point(30, 171);
            Airline.Name = "Airline";
            Airline.Size = new Size(124, 23);
            Airline.TabIndex = 29;
            Airline.Text = "VietNam Airlines";
            // 
            // hours
            // 
            hours.BackColor = Color.Transparent;
            hours.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hours.ForeColor = SystemColors.ControlDarkDark;
            hours.Location = new Point(328, 70);
            hours.Name = "hours";
            hours.Size = new Size(57, 23);
            hours.TabIndex = 28;
            hours.Text = "2h 30m";
            // 
            // ToDate
            // 
            ToDate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ToDate.AutoSize = false;
            ToDate.BackColor = Color.Transparent;
            ToDate.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ToDate.ForeColor = SystemColors.ControlDarkDark;
            ToDate.Location = new Point(593, 111);
            ToDate.Name = "ToDate";
            ToDate.Size = new Size(86, 23);
            ToDate.TabIndex = 27;
            ToDate.Text = "25/01/2026";
            // 
            // ToCode
            // 
            ToCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ToCode.AutoSize = false;
            ToCode.BackColor = Color.Transparent;
            ToCode.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ToCode.ForeColor = SystemColors.ControlDarkDark;
            ToCode.Location = new Point(568, 82);
            ToCode.Name = "ToCode";
            ToCode.Size = new Size(111, 23);
            ToCode.TabIndex = 26;
            ToCode.Text = "SGN";
            ToCode.TextAlignment = ContentAlignment.MiddleRight;
            // 
            // ToCity
            // 
            ToCity.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ToCity.AutoSize = false;
            ToCity.BackColor = Color.Transparent;
            ToCity.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ToCity.Location = new Point(525, 49);
            ToCity.Name = "ToCity";
            ToCity.Size = new Size(154, 27);
            ToCity.TabIndex = 25;
            ToCity.Text = "Hồ Chí Minh";
            ToCity.TextAlignment = ContentAlignment.MiddleRight;
            // 
            // ToHours
            // 
            ToHours.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ToHours.AutoSize = false;
            ToHours.BackColor = Color.Transparent;
            ToHours.Font = new Font("Segoe UI", 17.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ToHours.Location = new Point(614, 10);
            ToHours.Name = "ToHours";
            ToHours.Size = new Size(61, 33);
            ToHours.TabIndex = 24;
            ToHours.Text = "14:30";
            // 
            // guna2PictureBox2
            // 
            guna2PictureBox2.BackColor = Color.WhiteSmoke;
            guna2PictureBox2.CustomizableEdges = customizableEdges1;
            guna2PictureBox2.Image = Properties.Resources.horizontal_rule;
            guna2PictureBox2.ImageRotate = 0F;
            guna2PictureBox2.Location = new Point(30, 150);
            guna2PictureBox2.Margin = new Padding(3, 2, 3, 2);
            guna2PictureBox2.Name = "guna2PictureBox2";
            guna2PictureBox2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2PictureBox2.Size = new Size(649, 16);
            guna2PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2PictureBox2.TabIndex = 23;
            guna2PictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.WhiteSmoke;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(342, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.BackColor = Color.WhiteSmoke;
            guna2PictureBox1.CustomizableEdges = customizableEdges3;
            guna2PictureBox1.Image = Properties.Resources.horizontal_rule;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(271, 49);
            guna2PictureBox1.Margin = new Padding(3, 2, 3, 2);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2PictureBox1.Size = new Size(180, 16);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2PictureBox1.TabIndex = 21;
            guna2PictureBox1.TabStop = false;
            // 
            // FromDate
            // 
            FromDate.BackColor = Color.Transparent;
            FromDate.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FromDate.ForeColor = SystemColors.ControlDarkDark;
            FromDate.Location = new Point(28, 111);
            FromDate.Name = "FromDate";
            FromDate.Size = new Size(86, 23);
            FromDate.TabIndex = 3;
            FromDate.Text = "25/01/2026";
            // 
            // FromCode
            // 
            FromCode.BackColor = Color.Transparent;
            FromCode.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FromCode.ForeColor = SystemColors.ControlDarkDark;
            FromCode.Location = new Point(28, 82);
            FromCode.Name = "FromCode";
            FromCode.Size = new Size(35, 23);
            FromCode.TabIndex = 2;
            FromCode.Text = "SGN";
            FromCode.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // FromCity
            // 
            FromCity.BackColor = Color.Transparent;
            FromCity.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FromCity.Location = new Point(28, 49);
            FromCity.Name = "FromCity";
            FromCity.Size = new Size(111, 27);
            FromCity.TabIndex = 1;
            FromCity.Text = "Hồ Chí Minh";
            // 
            // FromHours
            // 
            FromHours.BackColor = Color.Transparent;
            FromHours.Font = new Font("Segoe UI", 17.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FromHours.Location = new Point(28, 10);
            FromHours.Name = "FromHours";
            FromHours.Size = new Size(61, 33);
            FromHours.TabIndex = 0;
            FromHours.Text = "14:30";
            FromHours.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // FlightDetailCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2Panel1);
            Name = "FlightDetailCard";
            Size = new Size(713, 213);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel FromCode;
        private Guna.UI2.WinForms.Guna2HtmlLabel FromCity;
        private Guna.UI2.WinForms.Guna2HtmlLabel FromHours;
        private Guna.UI2.WinForms.Guna2HtmlLabel FromDate;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel Prices;
        private Guna.UI2.WinForms.Guna2HtmlLabel Airline;
        private Guna.UI2.WinForms.Guna2HtmlLabel hours;
        private Guna.UI2.WinForms.Guna2HtmlLabel ToDate;
        private Guna.UI2.WinForms.Guna2HtmlLabel ToCode;
        private Guna.UI2.WinForms.Guna2HtmlLabel ToCity;
        private Guna.UI2.WinForms.Guna2HtmlLabel ToHours;
    }
}
