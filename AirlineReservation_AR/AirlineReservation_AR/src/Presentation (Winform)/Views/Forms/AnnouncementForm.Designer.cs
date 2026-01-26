using AirlineReservation_AR.Properties;
namespace AirlineReservation_AR.src.AirlineReservation.Presentation__WinForms_.Views.Forms.Common
{
    partial class AnnouncementForm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnnouncementForm));
            pictureBox1 = new PictureBox();
            titleLabel = new Label();
            subtitleLabel = new Label();
            completeBtn = new Guna.UI2.WinForms.Guna2Button();
            Closes = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Closes).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Resources.check;
            pictureBox1.Location = new Point(96, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(124, 72);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Fz Poppins SemBd", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.Location = new Point(0, 94);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(300, 30);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "SUCCESS";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // subtitleLabel
            // 
            subtitleLabel.Font = new Font("Fz Poppins", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            subtitleLabel.Location = new Point(0, 121);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(300, 30);
            subtitleLabel.TabIndex = 2;
            subtitleLabel.Text = "Sign In Successful!";
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // completeBtn
            // 
            completeBtn.BorderRadius = 5;
            completeBtn.CustomizableEdges = customizableEdges3;
            completeBtn.DisabledState.BorderColor = Color.DarkGray;
            completeBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            completeBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            completeBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            completeBtn.Font = new Font("Fz Poppins Med", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            completeBtn.ForeColor = Color.White;
            completeBtn.Location = new Point(108, 154);
            completeBtn.Name = "completeBtn";
            completeBtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            completeBtn.Size = new Size(85, 33);
            completeBtn.TabIndex = 7;
            completeBtn.Text = "OK";
            completeBtn.Visible = false;
            completeBtn.Click += completeBtn_Click;
            // 
            // Closes
            // 
            Closes.Image = (Image)resources.GetObject("Closes.Image");
            Closes.Location = new Point(268, 0);
            Closes.Name = "Closes";
            Closes.Size = new Size(32, 32);
            Closes.SizeMode = PictureBoxSizeMode.AutoSize;
            Closes.TabIndex = 8;
            Closes.TabStop = false;
            Closes.Click += pictureBox2_Click;
            // 
            // AnnouncementForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 200);
            Controls.Add(Closes);
            Controls.Add(completeBtn);
            Controls.Add(titleLabel);
            Controls.Add(pictureBox1);
            Controls.Add(subtitleLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AnnouncementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AnnouncementForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Closes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label titleLabel;
        private Label subtitleLabel;
        private Guna.UI2.WinForms.Guna2Button completeBtn;
        private PictureBox Closes;
    }
}