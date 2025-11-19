namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    partial class adminDasboard
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelTopBorder = new Panel();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            labelAdminDashboardControlDescription = new Label();
            labelAdminDashboardControlTitle = new Label();
            pictureBoxAdminDashboardControl = new PictureBox();
            TimerMove = new System.Windows.Forms.Timer(components);
            timerGradient = new System.Windows.Forms.Timer(components);
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAdminDashboardControl).BeginInit();
            SuspendLayout();
            // 
            // panelTopBorder
            // 
            panelTopBorder.Dock = DockStyle.Top;
            panelTopBorder.Location = new Point(0, 0);
            panelTopBorder.Margin = new Padding(0);
            panelTopBorder.Name = "panelTopBorder";
            panelTopBorder.Size = new Size(260, 4);
            panelTopBorder.TabIndex = 1;
            panelTopBorder.Paint += guna2Panel1_Paint;
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(labelAdminDashboardControlDescription);
            guna2Panel1.Controls.Add(labelAdminDashboardControlTitle);
            guna2Panel1.Controls.Add(pictureBoxAdminDashboardControl);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 4);
            guna2Panel1.Margin = new Padding(0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(260, 196);
            guna2Panel1.TabIndex = 2;
            // 
            // labelAdminDashboardControlDescription
            // 
            labelAdminDashboardControlDescription.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelAdminDashboardControlDescription.Location = new Point(25, 123);
            labelAdminDashboardControlDescription.Margin = new Padding(0);
            labelAdminDashboardControlDescription.Name = "labelAdminDashboardControlDescription";
            labelAdminDashboardControlDescription.Size = new Size(230, 50);
            labelAdminDashboardControlDescription.TabIndex = 2;
            labelAdminDashboardControlDescription.Text = "Chuyến bay, tuyến bay, sân bay, máy bay (All-in-one)";
            // 
            // labelAdminDashboardControlTitle
            // 
            labelAdminDashboardControlTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAdminDashboardControlTitle.ForeColor = Color.FromArgb(47, 147, 231);
            labelAdminDashboardControlTitle.Location = new Point(25, 85);
            labelAdminDashboardControlTitle.Margin = new Padding(0);
            labelAdminDashboardControlTitle.Name = "labelAdminDashboardControlTitle";
            labelAdminDashboardControlTitle.Size = new Size(230, 30);
            labelAdminDashboardControlTitle.TabIndex = 1;
            labelAdminDashboardControlTitle.Text = "Quản lý chuyến bay";
            // 
            // pictureBoxAdminDashboardControl
            // 
            pictureBoxAdminDashboardControl.Image = global::AirlineReservation_AR.Properties.Resources.take_off;
            pictureBoxAdminDashboardControl.Location = new Point(25, 20);
            pictureBoxAdminDashboardControl.Margin = new Padding(0);
            pictureBoxAdminDashboardControl.Name = "pictureBoxAdminDashboardControl";
            pictureBoxAdminDashboardControl.Size = new Size(50, 50);
            pictureBoxAdminDashboardControl.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAdminDashboardControl.TabIndex = 0;
            pictureBoxAdminDashboardControl.TabStop = false;
            pictureBoxAdminDashboardControl.MouseLeave += AnyControl_MouseLeave;
            pictureBoxAdminDashboardControl.MouseMove += AnyControl_MouseMove;
            // 
            // TimerMove
            // 
            TimerMove.Enabled = true;
            TimerMove.Tick += timerMoving_Tick;
            // 
            // timerGradient
            // 
            timerGradient.Interval = 10;
            timerGradient.Tick += timerGradient_Tick;
            // 
            // adminDasboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2Panel1);
            Controls.Add(panelTopBorder);
            Margin = new Padding(0);
            Name = "adminDasboard";
            Size = new Size(260, 200);
            Load += adminDasboard_Load;
            guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxAdminDashboardControl).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panelTopBorder;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label labelAdminDashboardControlDescription;
        private Label labelAdminDashboardControlTitle;
        private PictureBox pictureBoxAdminDashboardControl;
        private System.Windows.Forms.Timer TimerMove;
        private System.Windows.Forms.Timer timerGradient;
    }
}
