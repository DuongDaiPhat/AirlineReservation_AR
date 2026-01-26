using AirlineReservation_AR.src.Presentation__Winform_.Helpers;
namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class FlightCardControl
    {
        private System.ComponentModel.IContainer components = null;

        // UI controls - existing
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblAirline;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.Label lblPrice;
        private Guna.UI2.WinForms.Guna2Button btnSelect;
        private System.Windows.Forms.Label lblDuration;

        // UI controls - new for seat info
        private System.Windows.Forms.Label lblSeatsTitle;
        private BetterFlowLayoutPanel pnlSeatsInfo;

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

            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblAirline = new System.Windows.Forms.Label();
            this.lblTimes = new System.Windows.Forms.Label();
            this.lblRoute = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnSelect = new Guna.UI2.WinForms.Guna2Button();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblSeatsTitle = new System.Windows.Forms.Label();
            this.pnlSeatsInfo = new BetterFlowLayoutPanel();

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // picLogo - Logo hãng hàng không
            // ============================================================
            this.picLogo.Location = new System.Drawing.Point(15, 20);
            this.picLogo.Size = new System.Drawing.Size(40, 40);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.TabStop = false;

            // ============================================================
            // lblAirline - Tên hãng
            // ============================================================
            this.lblAirline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAirline.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblAirline.Location = new System.Drawing.Point(65, 20);
            this.lblAirline.Size = new System.Drawing.Size(150, 20);
            this.lblAirline.Text = "Thai AirAsia";
            this.lblAirline.BackColor = System.Drawing.Color.Transparent;

            // ============================================================
            // lblTimes - Giờ khởi hành → Giờ đến
            // ============================================================
            this.lblTimes.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTimes.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            this.lblTimes.Location = new System.Drawing.Point(65, 42);
            this.lblTimes.Size = new System.Drawing.Size(180, 30);
            this.lblTimes.Text = "09:45 → 11:30";
            this.lblTimes.BackColor = System.Drawing.Color.Transparent;

            // ============================================================
            // lblDuration - Thời gian bay
            // ============================================================
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.lblDuration.Location = new System.Drawing.Point(255, 25);
            this.lblDuration.Size = new System.Drawing.Size(90, 18);
            this.lblDuration.Text = "1h 35m";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;

            // ============================================================
            // lblRoute - Mã sân bay
            // ============================================================
            this.lblRoute.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblRoute.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.lblRoute.Location = new System.Drawing.Point(260, 45);
            this.lblRoute.Size = new System.Drawing.Size(80, 20);
            this.lblRoute.Text = "SGN";
            this.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoute.BackColor = System.Drawing.Color.Transparent;

            // ============================================================
            // lblPrice - Giá vé
            // ============================================================
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(255, 94, 20);
            this.lblPrice.Location = new System.Drawing.Point(380, 25);
            this.lblPrice.Size = new System.Drawing.Size(240, 30);
            this.lblPrice.Text = "2.512.916 VNĐ/khách";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;

            // ============================================================
            // btnSelect - Nút chọn chuyến bay
            // ============================================================
            this.btnSelect.Text = "Chọn";
            this.btnSelect.FillColor = System.Drawing.Color.FromArgb(1, 148, 243);
            this.btnSelect.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 130, 220);
            this.btnSelect.BorderRadius = 6;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(635, 20);
            this.btnSelect.Size = new System.Drawing.Size(90, 40);
            this.btnSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSelect.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSelect.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnSelect.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);

            // ============================================================
            // lblSeatsTitle - Tiêu đề "Ghế còn lại:"
            // ============================================================
            this.lblSeatsTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblSeatsTitle.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblSeatsTitle.Location = new System.Drawing.Point(15, 88);
            this.lblSeatsTitle.Size = new System.Drawing.Size(90, 20);
            this.lblSeatsTitle.Text = "Remain Seats:";
            this.lblSeatsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSeatsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ============================================================
            // pnlSeatsInfo - BetterFlowLayoutPanel chứa các badge ghế
            // ============================================================
            this.pnlSeatsInfo.Location = new System.Drawing.Point(110, 85);
            this.pnlSeatsInfo.Size = new System.Drawing.Size(615, 40);
            this.pnlSeatsInfo.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlSeatsInfo.WrapContents = true;
            this.pnlSeatsInfo.AutoScroll = false;
            this.pnlSeatsInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlSeatsInfo.Padding = new System.Windows.Forms.Padding(0);
            this.pnlSeatsInfo.Margin = new System.Windows.Forms.Padding(0);

            // ============================================================
            // Add Controls to Panel
            // ============================================================
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblAirline);
            this.Controls.Add(this.lblTimes);
            this.Controls.Add(this.lblRoute);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblSeatsTitle);
            this.Controls.Add(this.pnlSeatsInfo);

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
        }
    }
}