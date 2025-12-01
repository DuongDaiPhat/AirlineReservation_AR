namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UCRefundRescheduleContent
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
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnReschedule = new Guna.UI2.WinForms.Guna2Button();
            btnRefund = new Guna.UI2.WinForms.Guna2Button();
            guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.Location = new Point(17, 12);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(239, 34);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Refund - Reschedule";
            // 
            // btnReschedule
            // 
            btnReschedule.CustomizableEdges = customizableEdges1;
            btnReschedule.DisabledState.BorderColor = Color.DarkGray;
            btnReschedule.DisabledState.CustomBorderColor = Color.DarkGray;
            btnReschedule.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnReschedule.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnReschedule.FillColor = Color.White;
            btnReschedule.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            btnReschedule.ForeColor = Color.Black;
            btnReschedule.Image = Properties.Resources.arrow_right;
            btnReschedule.ImageAlign = HorizontalAlignment.Right;
            btnReschedule.ImageOffset = new Point(10, 0);
            btnReschedule.Location = new Point(0, 52);
            btnReschedule.Margin = new Padding(0);
            btnReschedule.Name = "btnReschedule";
            btnReschedule.Padding = new Padding(5, 0, 0, 0);
            btnReschedule.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnReschedule.Size = new Size(624, 64);
            btnReschedule.TabIndex = 1;
            btnReschedule.Text = "Reschedule";
            btnReschedule.TextAlign = HorizontalAlignment.Left;
            btnReschedule.Click += btnReschedule_Click;
            // 
            // btnRefund
            // 
            btnRefund.CustomizableEdges = customizableEdges3;
            btnRefund.DisabledState.BorderColor = Color.DarkGray;
            btnRefund.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRefund.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRefund.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRefund.FillColor = Color.White;
            btnRefund.Font = new Font("Segoe UI Semibold", 12.75F, FontStyle.Bold);
            btnRefund.ForeColor = Color.Black;
            btnRefund.Image = Properties.Resources.arrow_right1;
            btnRefund.ImageAlign = HorizontalAlignment.Right;
            btnRefund.ImageOffset = new Point(10, 0);
            btnRefund.Location = new Point(0, 126);
            btnRefund.Margin = new Padding(0);
            btnRefund.Name = "btnRefund";
            btnRefund.Padding = new Padding(5, 0, 0, 0);
            btnRefund.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRefund.Size = new Size(624, 64);
            btnRefund.TabIndex = 2;
            btnRefund.Text = "Refund";
            btnRefund.TextAlign = HorizontalAlignment.Left;
            // 
            // guna2Separator1
            // 
            guna2Separator1.Location = new Point(0, 116);
            guna2Separator1.Margin = new Padding(0);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(624, 10);
            guna2Separator1.TabIndex = 3;
            // 
            // UCRefundRescheduleContent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2Separator1);
            Controls.Add(btnRefund);
            Controls.Add(btnReschedule);
            Controls.Add(guna2HtmlLabel1);
            Name = "UCRefundRescheduleContent";
            Size = new Size(624, 619);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnReschedule;
        private Guna.UI2.WinForms.Guna2Button btnRefund;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
    }
}
