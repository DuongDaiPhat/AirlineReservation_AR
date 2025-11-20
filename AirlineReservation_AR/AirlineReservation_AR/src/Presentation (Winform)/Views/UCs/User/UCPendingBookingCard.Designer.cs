namespace AirlineReservation_AR.src.Presentation__Winform_.Views.Forms.User
{
    partial class UCPendingBookingCard
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
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            bntDetail = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.Location = new Point(19, 8);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(358, 33);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "TP HCM (SGN) -> Bangkok (BKK)";
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Location = new Point(19, 47);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(227, 22);
            guna2HtmlLabel3.TabIndex = 2;
            guna2HtmlLabel3.Text = "Mã đặt chỗ tên công ty: 29112005";
            // 
            // guna2Button1
            // 
            guna2Button1.BorderRadius = 10;
            guna2Button1.CustomizableEdges = customizableEdges5;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.DeepSkyBlue;
            guna2Button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(19, 86);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Button1.Size = new Size(320, 32);
            guna2Button1.TabIndex = 3;
            guna2Button1.Text = "Đang đợi phương thức thanh toán";
            // 
            // bntDetail
            // 
            bntDetail.BorderRadius = 10;
            bntDetail.CustomizableEdges = customizableEdges7;
            bntDetail.DisabledState.BorderColor = Color.DarkGray;
            bntDetail.DisabledState.CustomBorderColor = Color.DarkGray;
            bntDetail.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bntDetail.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bntDetail.FillColor = Color.Transparent;
            bntDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bntDetail.ForeColor = Color.DeepSkyBlue;
            bntDetail.Location = new Point(706, 86);
            bntDetail.Name = "bntDetail";
            bntDetail.ShadowDecoration.CustomizableEdges = customizableEdges8;
            bntDetail.Size = new Size(144, 32);
            bntDetail.TabIndex = 4;
            bntDetail.Text = "Xem chi tiết";
            // 
            // UCPendingBookingCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(bntDetail);
            Controls.Add(guna2Button1);
            Controls.Add(guna2HtmlLabel3);
            Controls.Add(guna2HtmlLabel1);
            Name = "UCPendingBookingCard";
            Size = new Size(853, 130);
            Load += UCPendingBookingCard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button bntDetail;
    }
}
