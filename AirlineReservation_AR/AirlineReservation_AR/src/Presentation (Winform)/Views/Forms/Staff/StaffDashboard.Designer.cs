namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.Staff
{
    partial class StaffDashboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            header_pnl = new Panel();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            uC_StaffHeader = new AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Staff.UC_StaffHeader();
            bodyPnl = new Guna.UI2.WinForms.Guna2Panel();
            header_pnl.SuspendLayout();
            SuspendLayout();
            // 
            // header_pnl
            // 
            header_pnl.Controls.Add(guna2Separator1);
            header_pnl.Controls.Add(uC_StaffHeader);
            header_pnl.Dock = DockStyle.Top;
            header_pnl.Location = new Point(0, 0);
            header_pnl.Name = "header_pnl";
            header_pnl.Size = new Size(1650, 100);
            header_pnl.TabIndex = 0;
            // 
            // guna2Separator1
            // 
            guna2Separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2Separator1.BackColor = Color.White;
            guna2Separator1.FillColor = Color.DimGray;
            guna2Separator1.FillThickness = 2;
            guna2Separator1.Location = new Point(0, 90);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(1650, 19);
            guna2Separator1.TabIndex = 0;
            // 
            // uC_StaffHeader
            // 
            uC_StaffHeader.BackColor = Color.White;
            uC_StaffHeader.Dock = DockStyle.Fill;
            uC_StaffHeader.Location = new Point(0, 0);
            uC_StaffHeader.Name = "uC_StaffHeader";
            uC_StaffHeader.Size = new Size(1650, 100);
            uC_StaffHeader.TabIndex = 0;
            // 
            // bodyPnl
            // 
            bodyPnl.BackColor = Color.White;
            bodyPnl.CustomizableEdges = customizableEdges1;
            bodyPnl.Dock = DockStyle.Fill;
            bodyPnl.Location = new Point(0, 100);
            bodyPnl.Name = "bodyPnl";
            bodyPnl.ShadowDecoration.CustomizableEdges = customizableEdges2;
            bodyPnl.Size = new Size(1650, 961);
            bodyPnl.TabIndex = 1;
            // 
            // StaffDashboard
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1650, 1061);
            Controls.Add(bodyPnl);
            Controls.Add(header_pnl);
            Name = "StaffDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StaffDashboard";
            Load += StaffDashboard_Load;
            header_pnl.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel header_pnl;
        private Guna.UI2.WinForms.Guna2Panel bodyPnl;
        private UCs.Staff.UC_StaffHeader uC_StaffHeader;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
    }
}