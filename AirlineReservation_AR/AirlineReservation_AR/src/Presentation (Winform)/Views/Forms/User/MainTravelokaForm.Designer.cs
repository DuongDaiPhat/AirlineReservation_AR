using AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User;

namespace AirlineReservation_AR
{
    partial class MainTravelokaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            ucHeader = new UC_Header();
            bodyPanel = new Panel();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(ucHeader);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1634, 100);
            headerPanel.TabIndex = 1;
            // 
            // ucHeader
            // 
            ucHeader.Dock = DockStyle.Fill;
            ucHeader.Location = new Point(0, 0);
            ucHeader.Name = "ucHeader";
            ucHeader.Size = new Size(1634, 100);
            ucHeader.TabIndex = 0;
            // 
            // bodyPanel
            // 
            bodyPanel.Dock = DockStyle.Fill;
            bodyPanel.Location = new Point(0, 100);
            bodyPanel.Name = "bodyPanel";
            bodyPanel.Size = new Size(1634, 941);
            bodyPanel.TabIndex = 0;
            // 
            // MainTravelokaForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1634, 1041);
            Controls.Add(bodyPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainTravelokaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Traveloka - Flight Booking UI";
            Load += MainTravelokaForm_Load;
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel bodyPanel;
        private UC_Header ucHeader;
        private UC_FlightSearch ucFlightSearch;
    }
}
