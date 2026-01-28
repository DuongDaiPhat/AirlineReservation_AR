namespace AirlineReservation_AR.src.Presentation__Winform_.Views.UCs.User
{
    partial class UserActivity
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
            operationIcon = new Guna.UI2.WinForms.Guna2PictureBox();
            Operation = new Guna.UI2.WinForms.Guna2HtmlLabel();
            time = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)operationIcon).BeginInit();
            SuspendLayout();
            // 
            // operationIcon
            // 
            operationIcon.CustomizableEdges = customizableEdges1;
            operationIcon.Image = Properties.Resources.user;
            operationIcon.ImageRotate = 0F;
            operationIcon.Location = new Point(17, 10);
            operationIcon.Name = "operationIcon";
            operationIcon.ShadowDecoration.CustomizableEdges = customizableEdges2;
            operationIcon.Size = new Size(41, 38);
            operationIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            operationIcon.TabIndex = 0;
            operationIcon.TabStop = false;
            // 
            // Operation
            // 
            Operation.BackColor = Color.Transparent;
            Operation.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Operation.Location = new Point(74, 10);
            Operation.Name = "Operation";
            Operation.Size = new Size(120, 25);
            Operation.TabIndex = 1;
            Operation.Text = "You registered";
            // 
            // time
            // 
            time.BackColor = Color.Transparent;
            time.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            time.ForeColor = SystemColors.ButtonShadow;
            time.Location = new Point(801, 37);
            time.Name = "time";
            time.Size = new Size(133, 23);
            time.TabIndex = 2;
            time.Text = "29/11/2026 00:16";
            time.TextAlignment = ContentAlignment.TopRight;
            // 
            // UserActivity
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(time);
            Controls.Add(Operation);
            Controls.Add(operationIcon);
            Name = "UserActivity";
            Size = new Size(937, 60);
            ((System.ComponentModel.ISupportInitialize)operationIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox operationIcon;
        private Guna.UI2.WinForms.Guna2HtmlLabel Operation;
        private Guna.UI2.WinForms.Guna2HtmlLabel time;
    }
}
