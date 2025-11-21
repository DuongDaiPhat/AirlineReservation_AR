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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtFromCtyToCty = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtBookingReference = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnPaymentStatus = new Guna.UI2.WinForms.Guna2Button();
            bntDetail = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // txtFromCtyToCty
            // 
            txtFromCtyToCty.BackColor = Color.Transparent;
            txtFromCtyToCty.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFromCtyToCty.Location = new Point(19, 8);
            txtFromCtyToCty.Name = "txtFromCtyToCty";
            txtFromCtyToCty.Size = new Size(358, 33);
            txtFromCtyToCty.TabIndex = 0;
            txtFromCtyToCty.Text = "TP HCM (SGN) -> Bangkok (BKK)";
            // 
            // txtBookingReference
            // 
            txtBookingReference.BackColor = Color.Transparent;
            txtBookingReference.Location = new Point(19, 47);
            txtBookingReference.Name = "txtBookingReference";
            txtBookingReference.Size = new Size(144, 22);
            txtBookingReference.TabIndex = 2;
            txtBookingReference.Text = "BookingID: 29112005";
            // 
            // btnPaymentStatus
            // 
            btnPaymentStatus.BorderRadius = 10;
            btnPaymentStatus.CustomizableEdges = customizableEdges7;
            btnPaymentStatus.DisabledState.BorderColor = Color.DarkGray;
            btnPaymentStatus.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPaymentStatus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPaymentStatus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPaymentStatus.FillColor = Color.DeepSkyBlue;
            btnPaymentStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPaymentStatus.ForeColor = Color.White;
            btnPaymentStatus.Location = new Point(19, 86);
            btnPaymentStatus.Name = "btnPaymentStatus";
            btnPaymentStatus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnPaymentStatus.Size = new Size(320, 32);
            btnPaymentStatus.TabIndex = 3;
            btnPaymentStatus.Text = "Waiting for payment method";
            // 
            // bntDetail
            // 
            bntDetail.BorderRadius = 10;
            bntDetail.CustomizableEdges = customizableEdges9;
            bntDetail.DisabledState.BorderColor = Color.DarkGray;
            bntDetail.DisabledState.CustomBorderColor = Color.DarkGray;
            bntDetail.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bntDetail.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bntDetail.FillColor = Color.Transparent;
            bntDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bntDetail.ForeColor = Color.DeepSkyBlue;
            bntDetail.Location = new Point(761, 86);
            bntDetail.Name = "bntDetail";
            bntDetail.ShadowDecoration.CustomizableEdges = customizableEdges10;
            bntDetail.Size = new Size(79, 32);
            bntDetail.TabIndex = 4;
            bntDetail.Text = "Detail";
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 10;
            btnCancel.CustomizableEdges = customizableEdges11;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.Transparent;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.Crimson;
            btnCancel.Location = new Point(676, 86);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancel.Size = new Size(79, 32);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            // 
            // UCPendingBookingCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnCancel);
            Controls.Add(bntDetail);
            Controls.Add(btnPaymentStatus);
            Controls.Add(txtBookingReference);
            Controls.Add(txtFromCtyToCty);
            Name = "UCPendingBookingCard";
            Size = new Size(853, 130);
            Load += UCPendingBookingCard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel txtFromCtyToCty;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtBookingReference;
        private Guna.UI2.WinForms.Guna2Button btnPaymentStatus;
        private Guna.UI2.WinForms.Guna2Button bntDetail;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}
