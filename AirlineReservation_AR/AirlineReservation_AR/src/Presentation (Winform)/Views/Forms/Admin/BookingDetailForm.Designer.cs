using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AirlineReservation_AR.src.Presentation__Winform_.Theme;

namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Admin
{
    partial class BookingDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Label lblTitle;
        private Panel pnlContent;
        private Button btnClose;

        // Info Labels
        private Label lblBookingRef;
        private Label lblStatus;
        private Label lblCustomer;
        private Label lblFlight;
        private Label lblAmount;

        private void InitializeComponent()
        {
            components = new Container();
            
            lblTitle = new Label();
            pnlContent = new Panel();
            btnClose = new Button();

            SuspendLayout();

            // Form
            this.Text = "Booking Details";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Title
            lblTitle.Text = "Booking Details";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = UIConstants.SidebarBg;
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;

            // Content Panel (Placeholder for dynamic content)
            pnlContent.Location = new Point(20, 60);
            pnlContent.Size = new Size(540, 540);
            pnlContent.AutoScroll = true;
            // pnlContent.BorderStyle = BorderStyle.FixedSingle;

            // Close Button
            btnClose.Text = "Close";
            btnClose.Location = new Point(240, 610);
            btnClose.Size = new Size(120, 40);
            btnClose.Click += (s, e) => this.Close();
            btnClose.BackColor = UIConstants.SidebarBg;
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;

            Controls.Add(lblTitle);
            Controls.Add(pnlContent);
            Controls.Add(btnClose);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
