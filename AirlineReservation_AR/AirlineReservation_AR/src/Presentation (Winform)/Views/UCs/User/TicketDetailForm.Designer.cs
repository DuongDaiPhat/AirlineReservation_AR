namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class TicketDetailForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.CustomizableEdges = customizableEdges5;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlHeader.Size = new Size(608, 45);
            pnlHeader.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.CustomizableEdges = customizableEdges7;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 45);
            pnlContent.Name = "pnlContent";
            pnlContent.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlContent.Size = new Size(608, 577);
            pnlContent.TabIndex = 1;
            // 
            // TicketDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 622);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Name = "TicketDetailForm";
            Text = "TicketDetailForm";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
    }
}