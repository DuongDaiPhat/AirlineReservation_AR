namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    partial class UCPaidTicket
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnDetail = new Guna.UI2.WinForms.Guna2Button();
            btnTicketStatus = new Guna.UI2.WinForms.Guna2Button();
            txtBookingReferences = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtFromCtyToCty = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtTakeOffTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtAirline_Airport_Terminal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtTicketPrice = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // btnDetail
            // 
            btnDetail.BorderRadius = 10;
            btnDetail.CustomizableEdges = customizableEdges5;
            btnDetail.DisabledState.BorderColor = Color.DarkGray;
            btnDetail.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDetail.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDetail.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDetail.FillColor = Color.Transparent;
            btnDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDetail.ForeColor = Color.DeepSkyBlue;
            btnDetail.Location = new Point(768, 3);
            btnDetail.Name = "btnDetail";
            btnDetail.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnDetail.Size = new Size(70, 32);
            btnDetail.TabIndex = 8;
            btnDetail.Text = "Detail";
            // 
            // btnTicketStatus
            // 
            btnTicketStatus.BorderRadius = 10;
            btnTicketStatus.CustomizableEdges = customizableEdges7;
            btnTicketStatus.DisabledState.BorderColor = Color.DarkGray;
            btnTicketStatus.DisabledState.CustomBorderColor = Color.DarkGray;
            btnTicketStatus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnTicketStatus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnTicketStatus.FillColor = Color.Green;
            btnTicketStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTicketStatus.ForeColor = Color.White;
            btnTicketStatus.Location = new Point(13, 127);
            btnTicketStatus.Name = "btnTicketStatus";
            btnTicketStatus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnTicketStatus.Size = new Size(227, 32);
            btnTicketStatus.TabIndex = 7;
            btnTicketStatus.Text = "E-ticket Issued";
            // 
            // txtBookingReferences
            // 
            txtBookingReferences.BackColor = Color.Transparent;
            txtBookingReferences.Location = new Point(13, 42);
            txtBookingReferences.Name = "txtBookingReferences";
            txtBookingReferences.Size = new Size(144, 22);
            txtBookingReferences.TabIndex = 6;
            txtBookingReferences.Text = "BookingID: 29112005";
            // 
            // txtFromCtyToCty
            // 
            txtFromCtyToCty.BackColor = Color.Transparent;
            txtFromCtyToCty.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFromCtyToCty.Location = new Point(13, 3);
            txtFromCtyToCty.Name = "txtFromCtyToCty";
            txtFromCtyToCty.Size = new Size(358, 33);
            txtFromCtyToCty.TabIndex = 5;
            txtFromCtyToCty.Text = "TP HCM (SGN) -> Bangkok (BKK)";
            // 
            // txtTakeOffTime
            // 
            txtTakeOffTime.BackColor = Color.Transparent;
            txtTakeOffTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtTakeOffTime.ForeColor = SystemColors.ControlDarkDark;
            txtTakeOffTime.Location = new Point(13, 70);
            txtTakeOffTime.Name = "txtTakeOffTime";
            txtTakeOffTime.Size = new Size(174, 22);
            txtTakeOffTime.TabIndex = 9;
            txtTakeOffTime.Text = "Fri, 29 Nov 2025 - 12:05";
            // 
            // txtAirline_Airport_Terminal
            // 
            txtAirline_Airport_Terminal.BackColor = Color.Transparent;
            txtAirline_Airport_Terminal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtAirline_Airport_Terminal.ForeColor = SystemColors.ControlDarkDark;
            txtAirline_Airport_Terminal.Location = new Point(13, 90);
            txtAirline_Airport_Terminal.Name = "txtAirline_Airport_Terminal";
            txtAirline_Airport_Terminal.Size = new Size(318, 22);
            txtAirline_Airport_Terminal.TabIndex = 10;
            txtAirline_Airport_Terminal.Text = "VietNam Airlines, Tan Son Nhat, Terminal 1B";
            // 
            // txtTicketPrice
            // 
            txtTicketPrice.BackColor = Color.Transparent;
            txtTicketPrice.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTicketPrice.ForeColor = Color.ForestGreen;
            txtTicketPrice.Location = new Point(709, 120);
            txtTicketPrice.Name = "txtTicketPrice";
            txtTicketPrice.Size = new Size(3, 2);
            txtTicketPrice.TabIndex = 11;
            // 
            // UCPaidTicket
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(txtTicketPrice);
            Controls.Add(txtAirline_Airport_Terminal);
            Controls.Add(txtTakeOffTime);
            Controls.Add(btnDetail);
            Controls.Add(btnTicketStatus);
            Controls.Add(txtBookingReferences);
            Controls.Add(txtFromCtyToCty);
            ForeColor = SystemColors.ControlDarkDark;
            Name = "UCPaidTicket";
            Size = new Size(853, 177);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnDetail;
        private Guna.UI2.WinForms.Guna2Button btnTicketStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtBookingReferences;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtFromCtyToCty;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtTakeOffTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtAirline_Airport_Terminal;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtTicketPrice;
    }
}
