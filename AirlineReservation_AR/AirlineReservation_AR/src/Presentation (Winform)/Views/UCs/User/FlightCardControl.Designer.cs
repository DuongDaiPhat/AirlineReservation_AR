namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class FlightCardControl
    {
        private System.ComponentModel.IContainer components = null;

        // UI controls
        private PictureBox picLogo;
        private Label lblAirline;
        private Label lblTimes;
        private Label lblRoute;
        private Label lblPrice;
        private Guna.UI2.WinForms.Guna2Button btnSelect;
        private Label lblDuration;

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
            this.components = new System.ComponentModel.Container();

            this.picLogo = new PictureBox();
            this.lblAirline = new Label();
            this.lblTimes = new Label();
            this.lblRoute = new Label();
            this.lblPrice = new Label();
            this.btnSelect = new Guna.UI2.WinForms.Guna2Button();
            this.lblDuration = new Label();

            this.SuspendLayout();

            // picLogo
            this.picLogo.Location = new System.Drawing.Point(15, 15);
            this.picLogo.Size = new System.Drawing.Size(35, 35);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = System.Drawing.Color.Transparent;

            // lblAirline
            this.lblAirline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAirline.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblAirline.Location = new System.Drawing.Point(60, 15);
            this.lblAirline.Size = new System.Drawing.Size(150, 20);
            this.lblAirline.Text = "Thai AirAsia";
            this.lblAirline.BackColor = System.Drawing.Color.Transparent;

            // lblTimes
            this.lblTimes.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTimes.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblTimes.Location = new System.Drawing.Point(60, 37);
            this.lblTimes.Size = new System.Drawing.Size(160, 30);
            this.lblTimes.Text = "09:45 → 11:30";
            this.lblTimes.BackColor = System.Drawing.Color.Transparent;

            // lblRoute
            this.lblRoute.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblRoute.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.lblRoute.Location = new System.Drawing.Point(230, 42);
            this.lblRoute.Size = new System.Drawing.Size(80, 20);
            this.lblRoute.Text = "SGN";
            this.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoute.BackColor = System.Drawing.Color.Transparent;

            // lblDuration
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.lblDuration.Location = new System.Drawing.Point(225, 23);
            this.lblDuration.Size = new System.Drawing.Size(90, 18);
            this.lblDuration.Text = "1h 45m";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;

            // lblPrice
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(255, 94, 20);
            this.lblPrice.Location = new System.Drawing.Point(430, 20);
            this.lblPrice.Size = new System.Drawing.Size(200, 30);
            this.lblPrice.Text = "4.627.260 VNĐ";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;

            // btnSelect
            this.btnSelect.Text = "Chọn";
            this.btnSelect.FillColor = System.Drawing.Color.FromArgb(1, 148, 243);
            this.btnSelect.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 130, 220);
            this.btnSelect.BorderRadius = 4;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(640, 17);
            this.btnSelect.Size = new System.Drawing.Size(80, 36);
            this.btnSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;

            // Add Controls
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblAirline);
            this.Controls.Add(this.lblTimes);
            this.Controls.Add(this.lblRoute);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnSelect);

            this.ResumeLayout(false);
        }
    }
}