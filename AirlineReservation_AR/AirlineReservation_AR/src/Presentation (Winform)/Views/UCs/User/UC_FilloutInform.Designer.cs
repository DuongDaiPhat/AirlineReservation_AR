namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UC_FilloutInform
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            contactPnl = new Guna.UI2.WinForms.Guna2Panel();
            pnlDetail = new Panel();
            guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            pnlSummary = new Panel();
            formPnl = new Helpers.BetterFlowLayoutPanel();
            betterFlowLayoutPanel2 = new Helpers.BetterFlowLayoutPanel();
            pnlDetail.SuspendLayout();
            betterFlowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // contactPnl
            // 
            contactPnl.BackColor = Color.Transparent;
            contactPnl.CustomizableEdges = customizableEdges1;
            contactPnl.Location = new Point(3, 3);
            contactPnl.Name = "contactPnl";
            contactPnl.ShadowDecoration.CustomizableEdges = customizableEdges2;
            contactPnl.Size = new Size(1073, 237);
            contactPnl.TabIndex = 1;
            // 
            // pnlDetail
            // 
            pnlDetail.Controls.Add(guna2Button2);
            pnlDetail.Controls.Add(guna2Button1);
            pnlDetail.Controls.Add(pnlSummary);
            pnlDetail.Location = new Point(1109, 3);
            pnlDetail.Name = "pnlDetail";
            pnlDetail.Size = new Size(512, 834);
            pnlDetail.TabIndex = 1;
            // 
            // guna2Button2
            // 
            guna2Button2.CustomizableEdges = customizableEdges3;
            guna2Button2.DisabledState.BorderColor = Color.DarkGray;
            guna2Button2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button2.Font = new Font("Segoe UI", 9F);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.Location = new Point(332, 538);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button2.Size = new Size(180, 45);
            guna2Button2.TabIndex = 2;
            guna2Button2.Text = "Chuyển đến trang thanh toán";
            guna2Button2.Click += guna2Button2_Click;
            // 
            // guna2Button1
            // 
            guna2Button1.CustomizableEdges = customizableEdges5;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(28, 538);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Button1.Size = new Size(180, 45);
            guna2Button1.TabIndex = 1;
            guna2Button1.Text = "Mở popup hành lý";
            guna2Button1.Click += btnAddBaggage_Click;
            // 
            // pnlSummary
            // 
            pnlSummary.Location = new Point(28, 19);
            pnlSummary.Name = "pnlSummary";
            pnlSummary.Size = new Size(472, 488);
            pnlSummary.TabIndex = 0;
            // 
            // formPnl
            // 
            formPnl.AutoScroll = true;
            formPnl.BackColor = Color.Transparent;
            formPnl.Location = new Point(3, 246);
            formPnl.Name = "formPnl";
            formPnl.Size = new Size(1073, 574);
            formPnl.TabIndex = 2;
            // 
            // betterFlowLayoutPanel2
            // 
            betterFlowLayoutPanel2.Controls.Add(contactPnl);
            betterFlowLayoutPanel2.Controls.Add(formPnl);
            betterFlowLayoutPanel2.Location = new Point(3, 6);
            betterFlowLayoutPanel2.Name = "betterFlowLayoutPanel2";
            betterFlowLayoutPanel2.Size = new Size(1076, 834);
            betterFlowLayoutPanel2.TabIndex = 2;
            // 
            // UC_FilloutInform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(betterFlowLayoutPanel2);
            Controls.Add(pnlDetail);
            Name = "UC_FilloutInform";
            Size = new Size(1650, 840);
            pnlDetail.ResumeLayout(false);
            betterFlowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel contactPnl;
        private Panel pnlDetail;
        private Panel pnlSummary;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Helpers.BetterFlowLayoutPanel formPnl;
        private Helpers.BetterFlowLayoutPanel betterFlowLayoutPanel2;
    }
}
