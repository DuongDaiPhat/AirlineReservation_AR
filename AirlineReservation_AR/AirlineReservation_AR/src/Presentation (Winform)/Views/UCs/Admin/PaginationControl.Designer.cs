namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.Admin
{
    partial class PaginationControl
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
            panelContainer = new Panel();
            btnNext = new Guna.UI2.WinForms.Guna2Button();
            btnPrevious = new Guna.UI2.WinForms.Guna2Button();
            panelContainer.SuspendLayout();
            SuspendLayout();
            // 
            // panelContainer
            // 
            panelContainer.Controls.Add(btnNext);
            panelContainer.Controls.Add(btnPrevious);
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(0, 0);
            panelContainer.Margin = new Padding(0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(1010, 50);
            panelContainer.TabIndex = 0;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Right;
            btnNext.BorderColor = Color.Gainsboro;
            btnNext.BorderRadius = 5;
            btnNext.Cursor = Cursors.Hand;
            btnNext.CustomizableEdges = customizableEdges1;
            btnNext.DisabledState.BorderColor = Color.DarkGray;
            btnNext.DisabledState.CustomBorderColor = Color.DarkGray;
            btnNext.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnNext.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnNext.FocusedColor = Color.FromArgb(150, 150, 150);
            btnNext.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(950, 4);
            btnNext.Margin = new Padding(0);
            btnNext.Name = "btnNext";
            btnNext.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnNext.Size = new Size(40, 40);
            btnNext.TabIndex = 1;
            btnNext.Text = ">";
            btnNext.Click += BtnNext_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Anchor = AnchorStyles.Right;
            btnPrevious.BorderColor = Color.Gainsboro;
            btnPrevious.BorderRadius = 5;
            btnPrevious.Cursor = Cursors.Hand;
            btnPrevious.CustomizableEdges = customizableEdges3;
            btnPrevious.DisabledState.BorderColor = Color.DarkGray;
            btnPrevious.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPrevious.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPrevious.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPrevious.FocusedColor = Color.FromArgb(150, 150, 150);
            btnPrevious.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPrevious.ForeColor = Color.White;
            btnPrevious.Location = new Point(20, 5);
            btnPrevious.Margin = new Padding(0);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPrevious.Size = new Size(40, 40);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "<";
            btnPrevious.Click += BtnPrevious_Click;
            // 
            // PaginationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelContainer);
            Margin = new Padding(0);
            Name = "PaginationControl";
            Size = new Size(1010, 50);
            panelContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelContainer;
        private Guna.UI2.WinForms.Guna2Button btnPrevious;
        private Guna.UI2.WinForms.Guna2Button btnNext;
    }
}
