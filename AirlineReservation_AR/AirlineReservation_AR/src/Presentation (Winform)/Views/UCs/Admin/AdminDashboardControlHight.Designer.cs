namespace AirlineReservation_AR.src.AirlineReservation.Presentation__Winform_.Views.UCs.Admin
{
    partial class AdminDashboardControlHight
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
            panelUserControlHightTop = new Panel();
            labelControlHight = new Label();
            picBoxControlHight = new PictureBox();
            panelUserControlHightTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBoxControlHight).BeginInit();
            SuspendLayout();
            // 
            // panelUserControlHightTop
            // 
            panelUserControlHightTop.Controls.Add(labelControlHight);
            panelUserControlHightTop.Controls.Add(picBoxControlHight);
            panelUserControlHightTop.Dock = DockStyle.Top;
            panelUserControlHightTop.Location = new Point(0, 0);
            panelUserControlHightTop.Margin = new Padding(0);
            panelUserControlHightTop.Name = "panelUserControlHightTop";
            panelUserControlHightTop.Size = new Size(1040, 77);
            panelUserControlHightTop.TabIndex = 0;
            // 
            // labelControlHight
            // 
            labelControlHight.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelControlHight.ImageAlign = ContentAlignment.MiddleLeft;
            labelControlHight.Location = new Point(110, 15);
            labelControlHight.Name = "labelControlHight";
            labelControlHight.Size = new Size(400, 50);
            labelControlHight.TabIndex = 1;
            labelControlHight.Text = "Admin Dashboard";
            labelControlHight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picBoxControlHight
            // 
            picBoxControlHight.Image = Properties.Resources.booking;
            picBoxControlHight.Location = new Point(40, 15);
            picBoxControlHight.Margin = new Padding(0);
            picBoxControlHight.Name = "picBoxControlHight";
            picBoxControlHight.Size = new Size(50, 50);
            picBoxControlHight.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxControlHight.TabIndex = 0;
            picBoxControlHight.TabStop = false;
            // 
            // AdminDashboardControlHight
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelUserControlHightTop);
            Margin = new Padding(0);
            Name = "AdminDashboardControlHight";
            Size = new Size(1040, 77);
            Load += AdminDashboardControlHight_Load;
            panelUserControlHightTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBoxControlHight).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelUserControlHightTop;
        private PictureBox picBoxControlHight;
        private Label labelControlHight;
    }
}
