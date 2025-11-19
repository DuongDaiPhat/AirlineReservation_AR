namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UC_Header
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Designer

        private void InitializeComponent()
        {
            pnlMain = new Panel();
            lblLogo = new Label();
            flowMenu = new FlowLayoutPanel();
            btnHotel = new Label();
            btnFlight = new Label();
            btnShuttle = new Label();
            btnCar = new Label();
            btnLogin = new Button();
            pnlMain.SuspendLayout();
            flowMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.Controls.Add(lblLogo);
            pnlMain.Controls.Add(flowMenu);
            pnlMain.Controls.Add(btnLogin);
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1440, 240);
            pnlMain.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(13, 110, 253);
            lblLogo.Location = new Point(29, 25);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(206, 43);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "traveloka ✈";
            // 
            // flowMenu
            // 
            flowMenu.AutoSize = true;
            flowMenu.Controls.Add(btnHotel);
            flowMenu.Controls.Add(btnFlight);
            flowMenu.Controls.Add(btnShuttle);
            flowMenu.Controls.Add(btnCar);
            flowMenu.Location = new Point(269, 25);
            flowMenu.Name = "flowMenu";
            flowMenu.Size = new Size(424, 57);
            flowMenu.TabIndex = 1;
            // 
            // btnHotel
            // 
            btnHotel.Location = new Point(3, 0);
            btnHotel.Name = "btnHotel";
            btnHotel.Size = new Size(100, 23);
            btnHotel.TabIndex = 0;
            btnHotel.Text = "Khách sạn";
            // 
            // btnFlight
            // 
            btnFlight.Location = new Point(109, 0);
            btnFlight.Name = "btnFlight";
            btnFlight.Size = new Size(100, 23);
            btnFlight.TabIndex = 1;
            btnFlight.Text = "Vé máy bay";
            // 
            // btnShuttle
            // 
            btnShuttle.Location = new Point(215, 0);
            btnShuttle.Name = "btnShuttle";
            btnShuttle.Size = new Size(100, 23);
            btnShuttle.TabIndex = 2;
            btnShuttle.Text = "Đưa đón sân bay";
            // 
            // btnCar
            // 
            btnCar.Location = new Point(321, 0);
            btnCar.Name = "btnCar";
            btnCar.Size = new Size(100, 23);
            btnCar.TabIndex = 3;
            btnCar.Text = "Thuê xe";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(13, 110, 253);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(1250, 25);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(124, 57);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // UC_Header
            // 
            Controls.Add(pnlMain);
            Name = "UC_Header";
            Size = new Size(1440, 240);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            flowMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.FlowLayoutPanel flowMenu;
        private System.Windows.Forms.Label btnHotel;
        private System.Windows.Forms.Label btnFlight;
        private System.Windows.Forms.Label btnShuttle;
        private System.Windows.Forms.Label btnCar;
        private System.Windows.Forms.Button btnLogin;
    }
}
