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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            txtFromCtyToCty.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFromCtyToCty.Location = new Point(17, 6);
            txtFromCtyToCty.Margin = new Padding(3, 2, 3, 2);
            txtFromCtyToCty.Name = "txtFromCtyToCty";
            txtFromCtyToCty.Size = new Size(425, 39);
            txtFromCtyToCty.TabIndex = 0;
            txtFromCtyToCty.Text = "TP HCM (SGN) -> Bangkok (BKK)";
            // 
            // txtBookingReference
            // 
            txtBookingReference.BackColor = Color.Transparent;
            txtBookingReference.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBookingReference.Location = new Point(17, 49);
            txtBookingReference.Margin = new Padding(3, 2, 3, 2);
            txtBookingReference.Name = "txtBookingReference";
            txtBookingReference.Size = new Size(163, 25);
            txtBookingReference.TabIndex = 2;
            txtBookingReference.Text = "BookingID: 29112005";
            // 
            // btnPaymentStatus
            // 
            btnPaymentStatus.BorderRadius = 10;
            btnPaymentStatus.CustomizableEdges = customizableEdges1;
            btnPaymentStatus.DisabledState.BorderColor = Color.DarkGray;
            btnPaymentStatus.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPaymentStatus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPaymentStatus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPaymentStatus.FillColor = Color.DeepSkyBlue;
            btnPaymentStatus.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold);
            btnPaymentStatus.ForeColor = Color.White;
            btnPaymentStatus.Location = new Point(17, 104);
            btnPaymentStatus.Margin = new Padding(3, 2, 3, 2);
            btnPaymentStatus.Name = "btnPaymentStatus";
            btnPaymentStatus.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPaymentStatus.Size = new Size(288, 34);
            btnPaymentStatus.TabIndex = 3;
            btnPaymentStatus.Text = "Waiting for payment method";
            // 
            // bntDetail
            // 
            bntDetail.BorderRadius = 10;
            bntDetail.CustomizableEdges = customizableEdges3;
            bntDetail.DisabledState.BorderColor = Color.DarkGray;
            bntDetail.DisabledState.CustomBorderColor = Color.DarkGray;
            bntDetail.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            bntDetail.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            bntDetail.FillColor = Color.Transparent;
            bntDetail.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold);
            bntDetail.ForeColor = Color.DeepSkyBlue;
            bntDetail.Location = new Point(836, 114);
            bntDetail.Margin = new Padding(3, 2, 3, 2);
            bntDetail.Name = "bntDetail";
            bntDetail.ShadowDecoration.CustomizableEdges = customizableEdges4;
            bntDetail.Size = new Size(111, 24);
            bntDetail.TabIndex = 4;
            bntDetail.Text = "Detail";
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 10;
            btnCancel.CustomizableEdges = customizableEdges5;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.Transparent;
            btnCancel.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Crimson;
            btnCancel.Location = new Point(738, 114);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCancel.Size = new Size(92, 24);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            // 
            // UCPendingBookingCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnCancel);
            Controls.Add(bntDetail);
            Controls.Add(btnPaymentStatus);
            Controls.Add(txtBookingReference);
            Controls.Add(txtFromCtyToCty);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UCPendingBookingCard";
            Size = new Size(947, 150);
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
